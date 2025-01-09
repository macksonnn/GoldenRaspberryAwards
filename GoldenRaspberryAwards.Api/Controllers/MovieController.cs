using GoldenRaspberryAwards.Application.Interface;
using GoldenRaspberryAwards.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GoldenRaspberryAwards.Presentation.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("producers-awards")]
        public async Task<IActionResult> GetProducersAwards()
        {
            try
            {
                var (min, max) = await _movieService.GetMinMaxIntervals();

                return Ok(new { min, max });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
