using EcommerceModel;

namespace BlazorEcommerce_Client.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetAll();
        public Task<ProductDTO> GetById(int productId);
    }
}
