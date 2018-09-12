using BlueVends.Shared.DTO.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.Order
{
    public class OrderDTO
    {
        public string Status { get; set; }
        public int TotalDue { get; set; }
        public string Address { get; set; }
        public System.DateTime OrderDate { get; set; }
        public IEnumerable<OrderProductDTO> Products { get; set; }
    }
}
