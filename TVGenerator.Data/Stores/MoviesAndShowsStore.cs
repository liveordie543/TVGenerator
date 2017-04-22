using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TVGenerator.Data.Models;

namespace TVGenerator.Data.Stores
{
    public class MoviesAndShowsStore
    {
        protected string _connectionString;

        public MoviesAndShowsStore()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IList<MovieOrShow> GetMoviesAndShows(DataFilter filter)
        {
            ConcurrentBag<MovieOrShow> moviesAndShows = new ConcurrentBag<MovieOrShow>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IncludeMovies", filter.IncludeMovies),
                new SqlParameter("@IncludeShows", filter.IncludeShows),
                new SqlParameter("@GenreIds", CreateGenreIdDataTable(filter.GenreIds))
            };

            using (CustomDataAdapter adapter = new CustomDataAdapter("GetMoviesAndShows", CommandType.StoredProcedure, parameters))
            {
                DataTable table = new DataTable();

                adapter.Fill(table);

                Parallel.ForEach(table.AsEnumerable(), row =>
                {
                    int showId = Convert.ToInt32(row["ShowId"]);
                    bool isMovie = Convert.ToBoolean(row["IsMovie"]);

                    moviesAndShows.Add(new MovieOrShow
                    {
                        Name = row["Name"] as string,
                        ShowId = showId,
                        IsMovie = isMovie,
                        Runtime = GetNullableInt(row["Runtime"]),
                        Genre = Convert.ToInt32(row["Genre"]),
                        Episodes = !isMovie ? GetEpisodesByShowId(showId) : new List<Episode>()
                    });
                });
            }

            return moviesAndShows.ToList();
        }

        private DataTable CreateGenreIdDataTable(List<int> genreIds)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Id", typeof(int));

            foreach (int genreId in genreIds)
            {
                table.Rows.Add(genreId);
            }

            return table;
        }

        private int? GetNullableInt(Object columnData)
        {
            if (columnData != Convert.DBNull)
            {
                return Convert.ToInt32(columnData);
            }

            return null;
        }

        private IList<Episode> GetEpisodesByShowId(int showId)
        {
            ConcurrentBag<Episode> episodes = new ConcurrentBag<Episode>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ShowId", showId)
            };

            using (CustomDataAdapter adapter = new CustomDataAdapter("GetEpisodesByShowId", CommandType.StoredProcedure, parameters))
            {
                DataTable table = new DataTable();

                adapter.Fill(table);

                Parallel.ForEach(table.AsEnumerable(), row =>
                {
                    episodes.Add(new Episode
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"] as string,
                        ShowId = Convert.ToInt32(row["ShowId"]),
                        Season = GetNullableInt(row["Season"]),
                        EpisodeNumber = GetNullableInt(row["EpisodeNumber"]),
                        EpisodeId = Convert.ToInt32(row["EpisodeId"])
                    });
                });
            }

            return episodes.ToList();
        }
    }
}
