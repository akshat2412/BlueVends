using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Shared.DTO.User
{
    public class UserBasicDTO
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
