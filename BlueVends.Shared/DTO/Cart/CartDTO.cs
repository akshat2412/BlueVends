using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Cart
{
    public class CartDTO
    {
        public int OrderQuantity { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid VariantID { get; set; }
    }
}
