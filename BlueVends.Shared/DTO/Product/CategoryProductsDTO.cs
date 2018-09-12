﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Product
{
    public class CategoryProductsDTO
    {
        public string Name { get; set; }
        public IEnumerable<ProductDTO> Products { get; set; }
    }
}
