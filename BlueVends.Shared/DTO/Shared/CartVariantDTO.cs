using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Variant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Shared
{
    public class CartVariantDTO
    {
        public string Name;
        public VariantDTO Variant;
        public Guid ID;
        public int OrderQuantity;
    }
}
