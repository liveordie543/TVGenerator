using System.Collections.Generic;

namespace TVGenerator.Data.Models
{
    public class MovieOrShow
    {
        public MovieOrShow()
        {
            Episodes = new List<Episode>();
        }

        public string Name { get; set; }

        public int ShowId { get; set; }

        public bool IsMovie { get; set; }

        public int? Runtime { get; set; }

        public int GenreId { get; set; }

        public IList<Episode> Episodes { get; set; }
    }
}
