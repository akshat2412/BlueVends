using BlueVends.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.User
{
    public class UserOrdersDTO
    {
        public IEnumerable<UserOrderDTO> Orders;
    }
}
