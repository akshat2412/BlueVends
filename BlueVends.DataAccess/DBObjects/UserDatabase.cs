using AutoMapper;
using BlueVends.DataAccess.Exceptions;
using BlueVends.DataAccess.Mappers.UserMappers;
using BlueVends.Entities;
using BlueVends.Shared.DTO.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BlueVends.DataAccess.DBObjects
{
    public class UserDatabase
    {
        BlueVendsDBEntities dbContext;
        IMapper _UserUserDTOmapper;
        IMapper _UserDTOUserMapper;
        IMapper _UserUserBasicDTOMapper;
        IMapper _OrdersUserOrdersDTOMapper;
        IMapper _OrderUserOrderDTOMapper;

        public UserDatabase()
        {
            dbContext = new BlueVendsDBEntities();

            _UserUserDTOmapper = Automappers.UserUserDTOmapper;
            _UserDTOUserMapper = Automappers.UserDTOUserMapper;
            _UserUserBasicDTOMapper = Automappers.UserUserBasicDTOMapper;
            _OrdersUserOrdersDTOMapper = Automappers.OrdersUserOrdersDTOMapper;
            _OrderUserOrderDTOMapper = Automappers.OrderUserOrderDTOMapper;
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
                UserBasicDTO newUserBasicDTO = _UserUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
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
            User user = _UserDTOUserMapper.Map<UserDTO, User>(userDTO);
            user.ID = Guid.NewGuid();
            user.Role.Add(dbContext.Role.Where(r => r.Name == "Normal").FirstOrDefault());
            dbContext.User.Add(user);
            dbContext.SaveChanges();
            UserBasicDTO newUserBasicDTO = _UserUserBasicDTOMapper.Map<User, UserBasicDTO>(user);
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
            newUserOrdersDTO.Orders = _OrdersUserOrdersDTOMapper.Map<IEnumerable<Order>, IEnumerable<UserOrderDTO>>(userOrders);
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

            UserOrderDTO newUserOrderDTO = _OrderUserOrderDTOMapper.Map<Order, UserOrderDTO>(order);
            //IEnumerable<ProductOrderJunction> newPOJ = order.ProductOrderJunction.Where(poj => poj.)
            //UserOrderDTO newUserOrdersDTO = new UserOrdersDTO();
            //newUserOrdersDTO.Orders = _OrdersUserOrdersDTOMapper.Map<IEnumerable<Order>, IEnumerable<UserOrderDTO>>(userOrders);
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
