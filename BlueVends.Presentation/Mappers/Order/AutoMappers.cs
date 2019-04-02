using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BlueVends.Presentation.ViewModels;
using BlueVends.Shared.DTO.Order;

namespace BlueVends.Presentation.Mappers.Order
{
    public class AutoMappers
    {
        public static IMapper AddressMapper
        {
            get
            {
                var AddressConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<AddressViewModel, AddressDTO>();
                });

                return new Mapper(AddressConfig);
            }
        }
        public static IMapper OrdersMapper
        {
            get
            {
                var OrdersConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<OrderDTO, OrderViewModel>().ForMember(x => x.Products, opt => opt.Ignore());
                    //cfg.CreateMap<OrderProductDTO, OrderProductViewModel>();
                    //cfg.CreateMap<ProductDTO, ProductViewModel>();
                    //cfg.CreateMap<OrderDTO, OrderViewModel>();
                });

                return new Mapper(OrdersConfig);
            }
        }
    }
}