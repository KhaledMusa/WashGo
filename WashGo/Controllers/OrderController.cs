using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashGo.Model;
using WashGo.Repository;

namespace WashGo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _orderRepository.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderID }, order);
        }

        [HttpPut("UpdateOrder/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.OrderID) return BadRequest();
            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }
    }

}

