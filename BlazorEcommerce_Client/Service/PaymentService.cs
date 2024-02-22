using BlazorEcommerce_Client.Service.IService;
using EcommerceModel;
using Newtonsoft.Json;
using System.Text;

namespace BlazorEcommerce_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
       
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessModelDTO> Checkout(StripePaymentDTO stripePaymentDTO)
        {
            try
            {
                var content = JsonConvert.SerializeObject(stripePaymentDTO);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDTO>(responseResult);
                   return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}. Error: {errorModel.ErrorMessage}");

                   // throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        
        }
        
           
    }
}
