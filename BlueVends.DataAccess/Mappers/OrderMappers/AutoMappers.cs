using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Order;

namespace BlueVends.DataAccess.Mappers.OrderMappers
{
    class AutoMappers
    {
        public static IMapper OrderMapper
        {
            get
            {
                var OrdersConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Order, OrderDTO>();
                });
                return new Mapper(OrdersConfig);
            }
        }
    }
}
