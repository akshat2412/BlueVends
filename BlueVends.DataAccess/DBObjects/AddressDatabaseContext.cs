using AutoMapper;
using BlueVends.DataAccess.Mappers.AddressMappers;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Order;
using System;

namespace BlueVends.DataAccess.DBObjects
{
    public class AddressDatabaseContext
    {
        BlueVendsDBEntities dbContext;
        IMapper _AddressMapper;

        public AddressDatabaseContext()
        {
            dbContext = new BlueVendsDBEntities();
            _AddressMapper = AutoMappers.AddressMapper;
        }

        public Guid AddAddress(Guid UserID, AddressDTO addressDTO)
        {
            Address address = _AddressMapper.Map<Address>(addressDTO);
            address.UserID = UserID;
            address.ID = Guid.NewGuid();
            dbContext.Address.Add(address);
            dbContext.SaveChanges();
            return address.ID;
        }
    }
}
