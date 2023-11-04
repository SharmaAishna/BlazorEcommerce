using AutoMapper;
using BlazorEcommerce_Business.Repository.IRepository;
using BlazorEcommerce_DataAccessLayer;
using BlazorEcommerce_DataAccessLayer.Data;
using EcommerceModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductPriceDTO> Create(ProductPriceDTO productPriceDTO)
        {
            var obj = _mapper.Map<ProductPriceDTO, ProductPrice>(productPriceDTO);
            var addedObj = _db.ProductPrices.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedObj.Entity);
        }

        public async Task<int> DeleteById(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(u => u.Id == id);
            if (obj != null)
            {
                _db.ProductPrices.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if (id != null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>
                    (_db.ProductPrices.Where(u => u.ProductId == id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductPrices);

            }
        }

        public async Task<ProductPriceDTO> GetById(int id)
        {
            var obj = await _db.ProductPrices.FirstOrDefaultAsync(p => p.Id == id);
            if (obj != null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO productPriceDTO)
        {
            var objFromDb = await _db.ProductPrices.FirstOrDefaultAsync(u => u.Id == productPriceDTO.Id);
            {
                if (objFromDb != null)
                {
                    objFromDb.Price = productPriceDTO.Price;
                    objFromDb.Size = productPriceDTO.Size;
                    objFromDb.ProductId = productPriceDTO.ProductId;
                    _db.ProductPrices.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<ProductPrice, ProductPriceDTO>(objFromDb);
                }
                return productPriceDTO;
            }
        }
    }
}
