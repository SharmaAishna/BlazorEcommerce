﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceModel
{
    public class SuccessModelDTO
    {
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public object Data {  get; set; }
    }
}
