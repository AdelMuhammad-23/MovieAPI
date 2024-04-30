namespace MovieApi.Services
{
    public interface IMovieServices
    {
       Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> GetById(int id);
        Task<Movie> Create (Movie movie);
        Movie Update (Movie movie);
        Movie Delete (Movie movie);

    }
}
