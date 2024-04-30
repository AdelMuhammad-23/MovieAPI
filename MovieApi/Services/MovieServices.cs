
namespace MovieApi.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Create(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return (movie);

        }

        public Movie Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte gnreId = 0)
        {
           return await _context.Movies
                .Where(m => m.GenreId == gnreId || gnreId == 0)
                .OrderByDescending(x => x.Rate)
                .Include(x => x.Genre)
                .ToListAsync();

        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Movie Update(Movie movie)
        {
             _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
