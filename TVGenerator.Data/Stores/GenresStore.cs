using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TVGenerator.Data.Models;

namespace TVGenerator.Data.Stores
{
    public class GenresStore
    {
        protected string _connectionString;

        public GenresStore()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IList<GenreOption> GetGenreOptions()
        {
            ConcurrentBag<GenreOption> genres = new ConcurrentBag<GenreOption>();

            using (CustomDataAdapter adapter = new CustomDataAdapter("GetGenres", CommandType.StoredProcedure))
            {
                DataTable table = new DataTable();

                adapter.Fill(table);

                Parallel.ForEach(table.AsEnumerable(), row =>
                {
                    genres.Add(new GenreOption
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Name = row["Name"] as String
                    });
                });
            }

            return genres.ToList();
        }
    }
}
