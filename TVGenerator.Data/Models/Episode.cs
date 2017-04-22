namespace TVGenerator.Data.Models
{
    public class Episode
    {
        public Episode()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int ShowId { get; set; }

        public int? Season { get; set; }

        public int? EpisodeNumber { get; set; }

        public int EpisodeId { get; set; }
    }
}
