﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceModel
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeaderDTO { get; set; }
        public List<OrderDetailDTO> OrderDetailsDTO { get; set; }
    }
}