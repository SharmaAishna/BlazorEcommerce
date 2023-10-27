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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
                _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Create(ProductDTO productDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(productDTO);
            var addedObj=_db.Products.Add(obj);
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);

        }

        public async Task<int> Delete(int id)
        {
          var obj=await _db.Products.FirstOrDefaultAsync(u=>u.Id==id);

            if (obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
           return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(u=>u.Category));
        }

        public async Task<ProductDTO> GetById(int id)
        {
           var obj = await _db.Products.Include(u =>u.Category).FirstOrDefaultAsync(p=>p.Id==id);
            if(obj != null)
            {
                return _mapper.Map<Product,ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<ProductDTO> Update(ProductDTO productDTO)
        {
            var objFromDb=await _db.Products.FirstOrDefaultAsync(u=>u.Id== productDTO.Id);
            {
                if (objFromDb != null)
                {
                    objFromDb.Name = productDTO.Name;
                    objFromDb.Description = productDTO.Description;
                    objFromDb.ImageUrl= productDTO.ImageUrl;
                    objFromDb.CategoryId = productDTO.CategoryId;
                    objFromDb.Color = productDTO.Color;
                    objFromDb.ShopFavorites = productDTO.ShopFavorites;
                    objFromDb.CustomerFavorite= productDTO.CustomerFavorite;    
                    _db.Products.Update(objFromDb);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<Product, ProductDTO>(objFromDb);
                }
                return productDTO;
            }
        }
    }
}
