using BlazorEcommerce_Business.Repository.IRepository;
using BlazorEcommerce_DataAccessLayer;
using BlazorEcommerce_DataAccessLayer.Data;
using EcommerceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public CategoryDTO Create(CategoryDTO objDTO)
        {
            Category category = new Category()
            {
                Name = objDTO.Name,
                Id = objDTO.Id,
                CreateDate = DateTime.Now
            };
            _db.Categories.Add(category);
            _db.SaveChanges();

            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public CategoryDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
            throw new NotImplementedException();
        }
    }
}
