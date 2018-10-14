using BlueVends.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlueVends.Shared.DTO.Order;

namespace BlueVends.DataAccess.DBObjects
{
    public class AddressDatabaseContext
    {
        BlueVendsDBEntities dbContext;
        IMapper AddressMapper;

        public AddressDatabaseContext()
        {
            dbContext = new BlueVendsDBEntities();
            var Config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AddressDTO, Address>();
            });
            AddressMapper = new Mapper(Config);
        }

        public Guid AddAddress(Guid UserID, AddressDTO addressDTO)
        {
            Address address = AddressMapper.Map<Address>(addressDTO);
            address.UserID = UserID;
            address.ID = Guid.NewGuid();
            dbContext.Address.Add(address);
            dbContext.SaveChanges();
            return address.ID;
        }
    }
}
