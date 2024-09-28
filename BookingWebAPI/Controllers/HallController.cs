using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HallController : Controller // Controller for hall requests
    {
        private readonly IHallRepository _hallRepository; // Interface for repository

        public HallController(IHallRepository repository) { _hallRepository = repository; }

        [HttpGet] // Get method for getting all concert halls
        public async Task<ActionResult<IEnumerable<ConcertHall>>> GetAll()
        {
            var _result = await _hallRepository.GetAll();

            return Ok(_result);
        }

        [HttpGet("{id}")] // Get method for getting concert hall by id
        public async Task<ActionResult<ConcertHall>> GetById(Guid id)
        {
            var _result = await _hallRepository.GetById(id);

            return Ok(_result);
        }

        [HttpPost] // Post method for creating an concert hall
        public async Task<ActionResult<String>> CreateHall([FromBody]ConcertHall hall)
        {
            hall.id = Guid.NewGuid();

            // If hall is already exists - stop creating
            var result = await _hallRepository.GetByName(hall.name);

            if(result == "")
            {
                return BadRequest(result);
            }

            return await _hallRepository.CreateAsync(hall);
        }

        [HttpPatch] // Update hall
        public async Task<IActionResult> UpdateHall([FromBody] ConcertHall hall)
        {
            var result = await _hallRepository.UpdateAsync(hall);

            return Ok(result);
        }

        [HttpDelete] // Delete hall (mark deletedAt, not complete removal from db)
        public async Task<IActionResult> DeleteHall([FromBody] Guid id)
        {
            var result = await _hallRepository.DeleteAsync(id);

            return Ok(result);
        }
    }
}
