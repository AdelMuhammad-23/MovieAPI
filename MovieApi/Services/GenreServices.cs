namespace MovieApi.Services
{
    public class GenreServices : IGenreServices
    {
        private readonly ApplicationDbContext _context;

        public GenreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckGenreId(byte id)
        {
            return await _context.Genres.AnyAsync(gen => gen.Id == id);
        }

        public async Task<Genre> Create(Genre genre)
        {
            _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
           return  await _context.Genres.OrderBy(gen => gen.Name).ToListAsync();
        }

        public async Task<Genre> GetById(byte id )
        {
           return await _context.Genres.SingleOrDefaultAsync(gen => gen.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
             _context.SaveChangesAsync();

            return genre;
        }
    }
}
