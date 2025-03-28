using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashGo.Model;
using WashGo.Repository;

namespace WashGo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllPayments()
        //{
        //    var payments = await _paymentRepository.GetAllPaymentsAsync();
        //    return Ok(payments);
        //}

        [HttpGet("GetPaymentById/{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpGet("GetPaymentsByOrderId/{orderId}")]
        public async Task<IActionResult> GetPaymentsByOrderId(int orderId)
        {
            var payments = await _paymentRepository.GetPaymentsByOrderIdAsync(orderId);
            return Ok(payments);
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _paymentRepository.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentID }, payment);
        }

        [HttpPut("UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            if (id != payment.PaymentID) return BadRequest();
            await _paymentRepository.UpdatePaymentAsync(payment);
            return NoContent();
        }

        [HttpDelete("DeletePayment/{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            await _paymentRepository.DeletePaymentAsync(id);
            return NoContent();
        }
    }
}
