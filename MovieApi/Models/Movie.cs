
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public int year { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }
    }

