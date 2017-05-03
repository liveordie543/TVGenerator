using System.Runtime.Serialization;

namespace TVGenerator.Data.Models
{
    [DataContract]
    public class GenreOption
    {
        public GenreOption()
        {
        }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "label")]
        public string Name { get; set; }
    }
}
