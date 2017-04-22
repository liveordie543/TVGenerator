using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TVGenerator.Data.Models
{
    [DataContract]
    public class DataFilter
    {
        public DataFilter()
        {
            GenreIds = new List<int>();
        }

        [DataMember]
        public bool IncludeMovies { get; set; }

        [DataMember]
        public bool IncludeShows { get; set; }

        [DataMember]
        public List<int> GenreIds { get; set; }
    }
}