using EcommerceModel;

namespace BlazorEcommerce_Client.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId = null);
        public Task<OrderDTO> GetById(int orderId);
    }
}
