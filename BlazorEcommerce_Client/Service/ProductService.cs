using BlazorEcommerce_Client.Service.IService;
using EcommerceModel;
using Newtonsoft.Json;

namespace BlazorEcommerce_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServiceUrl;

        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl");
        }
        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                return products;
            }
            return new List<ProductDTO>();
        }

        public async Task<ProductDTO> GetById(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }
    }
}
