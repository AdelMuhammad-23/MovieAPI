namespace MovieApi.Services
{
    public interface IGenreServices
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> Create(Genre genre);
        Task<bool> CheckGenreId (byte id);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);


    }
}
