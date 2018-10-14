using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueVends.Shared.DTO.User;
using BlueVends.Entities;
using AutoMapper;
using BlueVends.DataAccess.Exceptions;
using System.Data.Entity;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Shared;

namespace BlueVends.DataAccess.DBObjects
{
    public class UserDatabase
    {
        BlueVendsDBEntities dbContext;
        IMapper userUserDTOmapper;
        IMapper userDTOUserMapper;
        IMapper userUserBasicDTOMapper;
        IMapper ordersUserOrdersDTOMapper;
        IMapper orderUserOrderDTOMapper;
        public UserDatabase()
        {
            dbContext = new BlueVendsDBEntities();
            var userUserDTOconfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
            });

            var userDTOUserConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDTO, User>();
            });

            var userUserBasicDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserBasicDTO>();
            });

            var ordersUserOrdersDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, UserOrderDTO>();
                cfg.CreateMap<ProductOrderJunction, OrderProductDTO>();
            });

            var orderUserOrderDTOConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductOrderJunction, OrderProductDTO>();
                //cfg.CreateMap<IEnumerable<ProductOrderJunction>, IEnumerable<OrderProductDTO>>();
                cfg.CreateMap<Order, UserOrderDTO>();
            });



            userUserDTOmapper = new Mapper(userUserDTOconfig);
            userDTOUserMapper = new Mapper(userDTOUserConfig);
            userUserBasicDTOMapper = new Mapper(userUserBasicDTOConfig);
            ordersUserOrdersDTOMapper = new Mapper(ordersUserOrdersDTOConfig);
            orderUserOrderDTOMapper = new Mapper(orderUserOrderDTOConfig);
        }

        public bool UserExists(UserLoginDTO userLoginDTO)
        {
            User user = dbContext.User.Where(a => a.UserName == userLoginDTO.UserName).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public UserBasicDTO GetUser(UserLoginDTO userLoginDTO)
        {
            User user = dbContext.User.Where(a => a.UserName == userLoginDTO.UserName).First();
            if(user != null)
            {
                UserBasicDTO newUserBasicDTO = userUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
                return newUserBasicDTO;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public bool UserNameExists(string userName)
        {
            User user = dbContext.User.Where(a => a.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public UserBasicDTO AddUser(UserDTO userDTO)
        {
            User user = userDTOUserMapper.Map<UserDTO, User>(userDTO);
            user.ID = Guid.NewGuid();
            user.Role.Add(dbContext.Role.Where(r => r.Name == "Normal").FirstOrDefault());
            dbContext.User.Add(user);
            dbContext.SaveChanges();
            UserBasicDTO newUserBasicDTO = userUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
            return newUserBasicDTO;
        }

        public UserOrdersDTO GetOrders(Guid userID)
        {
            IEnumerable<Order> userOrders = dbContext.User.Where(u=>u.ID == userID).SelectMany(u => u.Order);
            if (userOrders == null)
            {
                throw new UserNotFoundException();
            }
            UserOrdersDTO newUserOrdersDTO = new UserOrdersDTO();
            newUserOrdersDTO.Orders = ordersUserOrdersDTOMapper.Map<IEnumerable<Order>, IEnumerable<UserOrderDTO>>(userOrders);
            //IEnumerable<Order> userOrders = user.Order;
            //foreach(var order in userOrders)
            //{
            //    var products = order.ProductOrderJunction.Select(poj => poj.Product);
            //}
            return newUserOrdersDTO;
        }

        public UserOrderDTO GetOrder(Guid orderID)
        {
            Order order = dbContext.Order.Where(o => o.ID == orderID).Include(o => o.ProductOrderJunction).Include(o => o.ProductOrderJunction.Select(poj => poj.Product)).FirstOrDefault();
            if (order == null)
            {
                throw new UserNotFoundException();
            }

            UserOrderDTO newUserOrderDTO = orderUserOrderDTOMapper.Map<Order, UserOrderDTO>(order);
            //IEnumerable<ProductOrderJunction> newPOJ = order.ProductOrderJunction.Where(poj => poj.)
            //UserOrderDTO newUserOrdersDTO = new UserOrdersDTO();
            //newUserOrdersDTO.Orders = ordersUserOrdersDTOMapper.Map<IEnumerable<Order>, IEnumerable<UserOrderDTO>>(userOrders);
            //IEnumerable<Order> userOrders = user.Order;
            //foreach(var order in userOrders)
            //{
            //    var products = order.ProductOrderJunction.Select(poj => poj.Product);
            //}
            //return newUserOrdersDTO;
            return newUserOrderDTO;
        }

        public bool CheckAdmin(Guid UserID)
        {
            User user = dbContext.User.Where(u => u.ID == UserID).Include(u => u.Role).First();
            foreach(var role in user.Role)
            {
                if(role.Name == "Admin")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
