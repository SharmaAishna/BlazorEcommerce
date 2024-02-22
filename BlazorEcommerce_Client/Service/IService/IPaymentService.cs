using EcommerceModel;
using System.ComponentModel.Design;

namespace BlazorEcommerce_Client.Service.IService
{
    public interface IPaymentService
    {
      public Task<SuccessModelDTO>  Checkout(StripePaymentDTO stripePaymentDTO);
    }
}
