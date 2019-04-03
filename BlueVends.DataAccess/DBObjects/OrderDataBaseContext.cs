using AutoMapper;
using BlueVends.DataAccess.Mappers.OrderMappers;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueVends.DataAccess.DBObjects
{
    public class OrderDataBaseContext
    {
        BlueVendsDBEntities dbContext;
        IMapper _OrderMapper;

        public OrderDataBaseContext()
        {
            dbContext = new BlueVendsDBEntities();
            _OrderMapper = AutoMappers.OrderMapper;
        }

        public void PlaceOrder(Guid userID, CartsDTO cartsDTO, Guid addressID)
        {
            Order order = new Order { ID = Guid.NewGuid(), AddressID = addressID, Status = "Completed", TotalDue = cartsDTO.SubTotal, UserID = userID, OrderDate = DateTime.Now };
            dbContext.Order.Add(order);
            dbContext.SaveChanges();
            foreach (var cartItem in cartsDTO.CartItems)
            {
                ProductOrderJunction poj = new ProductOrderJunction { OrderID = order.ID, OrderQuantity = cartItem.OrderQuantity, ProductID = cartItem.Variant.Product.ID, Variant = cartItem.Variant.Name, SellingPrice = cartItem.Variant.Product.DiscountedPrice };
                dbContext.ProductOrderJunction.Add(poj);
                dbContext.SaveChanges();
            }
            return;
        }

        public OrdersDTO GetOrders(Guid UserID)
        {
            var Orders = dbContext.Order.Where(o => o.UserID == UserID) ;
            OrdersDTO ordersDTO = new OrdersDTO();
            ordersDTO.Orders = _OrderMapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Orders);
            return ordersDTO;
        }
    }
}
