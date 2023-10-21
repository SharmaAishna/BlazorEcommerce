using AutoMapper;
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
        private readonly IMapper _mapper;
        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }


        public CategoryDTO Create(CategoryDTO objDTO)
        {
            var obj=_mapper.Map<CategoryDTO, Category>(objDTO);
            obj.CreateDate = DateTime.Now;
            //    Category category = new Category()
            //{
            //    Name = objDTO.Name,
            //    Id = objDTO.Id,
            //    CreateDate = DateTime.Now
            //};
          var addedObj=  _db.Categories.Add(obj);
            _db.SaveChanges();
            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
            //return new CategoryDTO()
            //{
            //    Id = category.Id,
            //    Name = category.Name
            //};
        }
 
        public int Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if(obj != null)
            {
                _db.Categories.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;

        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public CategoryDTO GetById(int id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj != null)
            {
                
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return new CategoryDTO();

        }

        public CategoryDTO Update(CategoryDTO objDTO)
        {
           var obj=_db.Categories.FirstOrDefault(u=>u.Id== objDTO.Id);
            if (obj != null)
            {//we will not update the id and created date.
                obj.Name= objDTO.Name;
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return _mapper.Map<Category, CategoryDTO>(obj);
            }
            return objDTO;
        }
    }
}
