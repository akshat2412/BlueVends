using BlueVends.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Shared
{
    public class OrderProductDTO
    {
        public string Name { get; set; }
        public int SellingPrice { get; set; }
        public string Variant { get; set; }
        public int Quantity { get; set; }
        public System.Guid ProductID { get; set; }
        public ProductDTO Product { get; set;}
    }
}
