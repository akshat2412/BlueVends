using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Order;

namespace BlueVends.DataAccess.Mappers.AddressMappers
{
    class AutoMappers
    {
        public static IMapper AddressMapper
        {
            get
            {
                var AddressConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AddressDTO, Address>();
                });
                return new Mapper(AddressConfig);
            }
        }
    }
}
