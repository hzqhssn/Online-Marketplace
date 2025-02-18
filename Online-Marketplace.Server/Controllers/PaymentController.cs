using Microsoft.AspNetCore.Mvc;
using OnlineMarketplace.Server.Interface;
using OnlineMarketplace.Server.Models;
using System.Threading.Tasks;

namespace OnlineMarketplace.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            await _paymentService.ProcessPaymentAsync(paymentDto);
            return Ok(new { message = "Payment processed successfully." });
        }
    }
}
