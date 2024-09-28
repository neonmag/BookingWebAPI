using BookingWebAPI.Entity.Orders;
using BookingWebAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller // Controller for order requests
    {
        private readonly IOrderRepository _orderRepository; // Interface for repository

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet] // Get method for getting all orders
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var result = await _orderRepository.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")] // Get order by id
        public async Task<ActionResult<Order>> GetById(Guid id)
        {
            var result = await _orderRepository.GetById(id);

            return Ok(result);
        }

        [HttpPost] // Make an order
        public async Task<ActionResult<String>> CreateOrder([FromBody] Order order)
        {
            order.id = Guid.NewGuid();

            var result = await _orderRepository.CreateAsync(order);

            return Ok(result);
        }

        [HttpPatch] // Update an order
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var result = await _orderRepository.UpdateAsync(order);

            return Ok(result);
        }

        [HttpDelete] // Delete an order (mark deletedAt, not complete removal from db)
        public async Task<IActionResult> DeleteOrder([FromBody] Guid id)
        {
            var result = await _orderRepository.DeleteAsync(id);

            return Ok(result);
        }

        [HttpGet("available-halls")] // Get all available halls to order by setted time and capacity of hall
        public async Task<IActionResult> GetAvailableHalls(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            var availableHalls = await _orderRepository.GetAvailableOrders(date, startTime, endTime, capacity);

            if (availableHalls.Any())
            {
                return Ok(availableHalls);
            }

            return NotFound("No available halls for the given time and capacity");
        }

    }
}
