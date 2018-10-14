using BlueVends.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Cart
{
    public class CartsDTO
    {   
        public int SubTotal { get; set; }
        public IEnumerable<CartVariantDTO> CartItems { get; set; }
    }
}
