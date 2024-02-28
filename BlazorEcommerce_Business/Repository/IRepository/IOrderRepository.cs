using EcommerceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> GetById(int id);
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId = null, string? status = null);
        public Task<OrderDTO> Create(OrderDTO orderDTO);
        public Task<int> DeleteById(int id);
        public Task<OrderHeaderDTO> UpdateHeader(OrderHeaderDTO orderHeaderDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id,string paymentIntentId);
        public Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
