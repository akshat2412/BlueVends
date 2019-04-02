using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Order;
using BlueVends.Shared.DTO.Shared;

namespace BlueVends.Presentation.Mappers.User
{
    public class AutoMappers
    {
        public static IMapper OrdersVMMapper
        {
            get
            {
                var OrdersVMConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<OrderDTO, OrderViewModel>();
                    cfg.CreateMap<OrderProductDTO, OrderProductViewModel>();
                });

                return new Mapper(OrdersVMConfig);
            }
        }
    }
}