﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce_DataAccessLayer.ViewModel
{
    public class Order
    {
        public OrderHeader? OrderHeader { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
    }
}
