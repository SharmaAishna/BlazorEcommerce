using BlazorEcommerce_Business.Repository.IRepository;
using BlazorEcommerce_Common;
using EcommerceModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;


namespace Blazor_Ecommerce_WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly IEmailSender _emailSender;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
      //      _emailSender = emailSender;

        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="orderHeaderId"></param>
        /// <returns></returns>

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> GetById(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });

            }
            var orderHeader = await _orderRepository.GetById(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(orderHeader);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDTO.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id,
                    sessionDetails.PaymentIntentId);
                //await _emailSender.SendEmailAsync(orderHeaderDTO.Email,
                //    "Order Confirmation",
                //    "New Order has been created:" + orderHeaderDTO.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDTO()
                    {
                        ErrorMessage = "Can't mark payment as successfull"
                    });

                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
