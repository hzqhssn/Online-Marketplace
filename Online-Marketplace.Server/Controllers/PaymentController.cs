using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineMarketplace.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

       

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        // ✅ Create Payment Session
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] List<CartItem> cart)
        {
            var domain = "http://localhost:3000"; // Update this when deploying

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{domain}/success",
                CancelUrl = $"{domain}/cart"
            };

            foreach (var item in cart)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name
                        },
                        UnitAmount = (long)(item.Price * 100) // Convert to cents
                    },
                    Quantity = item.Quantity
                });
            }

            var service = new Stripe.Checkout.SessionService(); 
            Stripe.Checkout.Session session = await service.CreateAsync(options);

            return Ok(new { sessionUrl = session.Url });
        }
    }

    // ✅ Define CartItem Model
    public class CartItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
