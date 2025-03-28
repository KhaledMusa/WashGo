using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashGo.Model;
using WashGo.Repository;

namespace WashGo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet("GetOrderItemById/{id}")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemRepository.GetOrderItemByIdAsync(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        [HttpGet("GetOrderItemsByOrderId/{orderId}")]
        public async Task<IActionResult> GetOrderItemsByOrderId(int orderId)
        {
            var items = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
            return Ok(items);
        }

        [HttpPost("CreateOrderItem")]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _orderItemRepository.AddOrderItemAsync(orderItem);
            return Ok(orderItem);
        }

        [HttpPut("UpdateOrderItem/{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItem orderItem)
        {
            if (id != orderItem.OrderItemID) return BadRequest();
            await _orderItemRepository.UpdateOrderItemAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("DeleteOrderItem/{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemRepository.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
    
}
