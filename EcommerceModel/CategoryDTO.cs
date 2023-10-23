﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceModel
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter name..")]
        public string Name { get; set; }
    }
}
