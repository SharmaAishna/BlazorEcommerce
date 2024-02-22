using EcommerceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Blazor_Ecommerce_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [ActionName("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO stripePaymentDTO)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Client_Url");
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    SuccessUrl = domain + stripePaymentDTO.SuccessUrl,
                    CancelUrl = domain + stripePaymentDTO.CancelUrl,
                    LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                   // {
                    //    new Stripe.Checkout.SessionLineItemOptions()
                        //{
                        //    Price = "price_1MotwRLkdIwHu7ixYcPLm5uZ",
                        //    Quantity = 2,
                        //},
                 //   },
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                foreach (var item in stripePaymentDTO.Order.OrderDetails)
                {
                    var sessionLineItem = new Stripe.Checkout.SessionLineItemOptions
                    {
                        PriceData = new Stripe.Checkout.SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),//20.00->2000,in our database is in dollars in stripe its in long.therefore *100 
                            Currency = "USD",
                            ProductData = new Stripe.Checkout.SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }

                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                    var service = new Stripe.Checkout.SessionService();
                    Session session = service.Create(options);
                    return Ok(new SuccessModelDTO()
                    {
                        Data = session.Id
                    });
                                
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = ex.Message
                });
            }

        }
    }
}
