using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieApi.Helper;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices _movieServices;
        private readonly IGenreServices _genreServices;
        private readonly IMapper _mapper;
        private new List<string> ExtentionsAllowed = new List<string> { ".jpg", ".png" };
        private long allowedPosterSize = 1024 * 1024;


        public MoviesController(IGenreServices genreServices, IMovieServices movieServices, IMapper mapper)
        {
            _genreServices = genreServices;
            _movieServices = movieServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMovieDto dto)
        {
            if (dto.Poster is null) 
                return BadRequest("The Poster field is required.");
            
            if (!ExtentionsAllowed.Contains(Path.GetExtension(dto.Poster.FileName).ToLower())) 
                return BadRequest("This Extentions Not Allowed only (.jpg , .png)!");

            if (dto.Poster.Length > allowedPosterSize)
                return BadRequest("Max Size allowed of Poster is 1MB!");

            var CheckGenreId = await _genreServices.CheckGenreId(dto.GenreId);
            if (!CheckGenreId)

                return BadRequest("Invalid Genere Id");


            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray(); 

            _movieServices.Create(movie);

            return Ok(movie);

        }

        [HttpGet]   
        public async Task<IActionResult> GetAllAsync()
        {
            var GetMovies = await _movieServices.GetAll();

            var Movies = _mapper.Map<IEnumerable<MovieDetailsDto>>(GetMovies);

            
                return Ok(Movies);
        } [HttpGet("{id}")]   
        public async Task<IActionResult> GetMovieById(int id)
        {
            var Movies = await _movieServices.GetById(id);

            if (Movies is null)
                return NotFound();

            var data = _mapper.Map<MovieDetailsDto>(Movies);
            
                return Ok(data);
        }
        [HttpGet("GetByGenereId")]
        public async Task<IActionResult> GetByGenereIdAsync(byte genereId)
        {
            var Movies = await _movieServices.GetAll(genereId);
            return Ok(Movies);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _movieServices.GetById(id);
            if (movie is null)
                return NotFound($"No genre was fount with ID:{id} ");

            _movieServices.Delete(movie);

            return Ok(movie);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id,[FromForm] UpdateMovieDto movie)
        {
            var Updatemovie = await _movieServices.GetById(id);
            if (Updatemovie is null)
                return NotFound($"No Movie was fount with ID: {id} ");

            var CheckGenreId = await _genreServices.CheckGenreId(movie.GenreId);
            if (!CheckGenreId)
                return BadRequest("Invalid Genere Id");


            if ( movie.Poster != null)
            {
                if (!ExtentionsAllowed.Contains(Path.GetExtension( movie.Poster.FileName).ToLower()))
                    return BadRequest("This Extentions Not Allowed only (.jpg , .png)!");

                if (movie.Poster.Length > allowedPosterSize)
                    return BadRequest("Max Size allowed of Poster is 1MB!");

                using var dataStream = new MemoryStream();
                 await movie.Poster.CopyToAsync(dataStream);

                Updatemovie.Poster = dataStream.ToArray();
            }

            Updatemovie.Title = movie.Title;
            Updatemovie.Rate = movie.Rate;
            Updatemovie.StoreLine = movie.StoreLine;
            Updatemovie.year = movie.year;
            Updatemovie.GenreId = movie.GenreId;

            _movieServices.Update(Updatemovie);
            return Ok(movie);
        }
    }
}
