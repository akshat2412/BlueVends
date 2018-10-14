using BlueVends.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Variant
{
    public class VariantDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ProductDTO Product;
    }
}
