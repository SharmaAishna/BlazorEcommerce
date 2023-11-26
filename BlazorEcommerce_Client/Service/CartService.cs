using BlazorEcommerce_Client.Service.IService;
using BlazorEcommerce_Client.Shared.ViewModels;
using BlazorEcommerce_Common;
using Blazored.LocalStorage;

namespace BlazorEcommerce_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorageService;
        public event Action OnChange;

        public CartService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public async Task DecrementCart(ShoppingCart CartToDecrement)
        {
            var cart = await _localStorageService.GetItemAsync<List<ShoppingCart>>(StaticDetails.ShoppingCart);

            //if count is 0 or 1 then we remove the item.
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == CartToDecrement.ProductId && cart[i].ProductPriceId == CartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count == 1 || CartToDecrement.Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= CartToDecrement.Count;
                    }
                }

            }
            await _localStorageService.SetItemAsync(StaticDetails.ShoppingCart, cart);
            OnChange.Invoke();
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
            }
            await _localStorageService.SetItemAsync(StaticDetails.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}
