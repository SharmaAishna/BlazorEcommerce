using EcommerceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_Business.IRepository
{
    public interface ICategoryRepository
    {
        public CategoryDTO Create(CategoryDTO objDTO);
        public CategoryDTO Update(CategoryDTO objDTO);
        public CategoryDTO Delete(int id);
        public List<CategoryDTO> GetAll();
        public CategoryDTO GetById(int id);
    }
}
