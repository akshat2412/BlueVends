using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.User;

namespace BlueVends.DataAccess.Mappers.UserMappers
{
    class Automappers
    {
        public static IMapper UserUserDTOmapper
        {
            get
            {
                var UserUserConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserDTO>();
                });
                return new Mapper(UserUserConfig);
            }
        }

        public static IMapper UserDTOUserMapper
        {
            get
            {
                var UserDTOUserConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<UserDTO, User>();
                });
                return new Mapper(UserDTOUserConfig);
            }
        }

        public static IMapper UserUserBasicDTOMapper
        {
            get
            {
                var UserUserBasicDTOConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<User, UserBasicDTO>();
                });
                return new Mapper(UserUserBasicDTOConfig);
            }
        }

        public static IMapper OrdersUserOrdersDTOMapper
        {
            get
            {
                var OrdersUserOrdersDTOConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Order, UserOrderDTO>();
                    cfg.CreateMap<ProductOrderJunction, OrderProductDTO>();
                });
                return new Mapper(OrdersUserOrdersDTOConfig);

            }
        }

        public static IMapper OrderUserOrderDTOMapper
        {
            get
            {
                var OrderUserOrderDTOConfig = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<ProductOrderJunction, OrderProductDTO>();
                    //cfg.CreateMap<IEnumerable<ProductOrderJunction>, IEnumerable<OrderProductDTO>>();
                    cfg.CreateMap<Order, UserOrderDTO>();
                });
                return new Mapper(OrderUserOrderDTOConfig);
            }
        }
    }
}
