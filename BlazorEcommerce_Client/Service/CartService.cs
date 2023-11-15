using Blazor_Ecommerce_Common;
using BlazorEcommerce_Client.Service.IService;
using BlazorEcommerce_Client.Shared.ViewModels;
using Blazored.LocalStorage;

namespace BlazorEcommerce_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public Task DecrementCart(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }

        public async Task IncrementCart(ShoppingCart cartToAdd)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(StaticDetails.ShoppingCart);
            bool itemInCart = false;
            if (cart == null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach (var obj in cart)
            {
                if (obj.ProductId == cartToAdd.ProductId && obj.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    obj.Count += cartToAdd.Count;
                }
            }
            if (!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
                await _localStorageService.SetItemAsync(StaticDetails.ShoppingCart, cart);
            }
        }
    }
}
