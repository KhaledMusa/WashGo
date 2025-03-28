using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashGo.Model;
using WashGo.Repository;

namespace WashGo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryController(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet("GetAllDeliveriesAsync")]
        public async Task<IActionResult> GetAllDeliveries()
        {
            var deliveries = await _deliveryRepository.GetAllDeliveriesAsync();
            return Ok(deliveries);
        }

        [HttpGet("GetDeliveryById/{id}")]
        public async Task<IActionResult> GetDeliveryById(int id)
        {
            var delivery = await _deliveryRepository.GetDeliveryByIdAsync(id);
            if (delivery == null) return NotFound();
            return Ok(delivery);
        }

        [HttpGet("GetDeliveriesByDriverId/{driverId}")]
        public async Task<IActionResult> GetDeliveriesByDriverId(int driverId)
        {
            var deliveries = await _deliveryRepository.GetDeliveriesByDriverIdAsync(driverId);
            return Ok(deliveries);
        }

        [HttpPost("CreateDelivery")]
        public async Task<IActionResult> CreateDelivery([FromBody] Delivery delivery)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _deliveryRepository.AddDeliveryAsync(delivery);
            return CreatedAtAction(nameof(GetDeliveryById), new { id = delivery.DeliveryID }, delivery);
        }

        [HttpPut("UpdateDelivery/{id}")]
        public async Task<IActionResult> UpdateDelivery(int id, [FromBody] Delivery delivery)
        {
            if (id != delivery.DeliveryID) return BadRequest();
            await _deliveryRepository.UpdateDeliveryAsync(delivery);
            return NoContent();
        }

        [HttpDelete("DeleteDelivery/{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            await _deliveryRepository.DeleteDeliveryAsync(id);
            return NoContent();
        }
    }
}
