using BlazorEcommerce_Client.Shared.ViewModels;

namespace BlazorEcommerce_Client.Service.IService
{
    public interface ICartService
    {
        Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);
    }
}
