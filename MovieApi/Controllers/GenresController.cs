using Microsoft.AspNetCore.Http;


namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenresController( IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await  _genreServices.GetAll();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenresDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name
            };

            await _genreServices.Create(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(byte id, [FromBody] GenresDto dto)

        {
            var genre = await _genreServices.GetById(id);

            if (genre is null)
                return NotFound($"No genre was fount with ID:{id} ");

            _genreServices.Update( genre);

            return Ok(genre);
        }
        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeleteGenre(byte id )
        {
            var genre = await _genreServices.GetById(id);

            if (genre is null)
                return NotFound($"No genre was fount with ID:{id} ");

                 _genreServices.Delete(genre);
            return Ok(genre);
        }
    }
}
