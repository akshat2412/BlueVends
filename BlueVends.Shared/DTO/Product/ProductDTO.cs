using BlueVends.Shared.DTO.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Product
{
    public class ProductDTO
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public int ListingPrice { get; set; }
        public int Inventory { get; set; }
        public int QuantitySold { get; set; }
        public int OrderLimit { get; set; }
        public string ImageURL { get; set; }
        public IEnumerable<VariantDTO> Variants;
        public System.Guid CategoryID { get; set; }
    }
}
