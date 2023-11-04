using EcommerceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.Repository.IRepository
{
    public interface IProductPriceRepository
    {
        public Task<IEnumerable<ProductPriceDTO>> GetAll(int? id=null);
        public Task<ProductPriceDTO> GetById(int id);
        public Task<ProductPriceDTO> Create(ProductPriceDTO productPrice);
        public Task<ProductPriceDTO> Update(ProductPriceDTO productPrice);
        public Task<int> DeleteById(int id);
    }
}
