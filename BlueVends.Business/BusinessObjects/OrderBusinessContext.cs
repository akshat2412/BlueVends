using BlueVends.Business.Exceptions;
using BlueVends.DataAccess.DBObjects;
using BlueVends.Shared.DTO.Cart;
using BlueVends.Shared.DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Business.BusinessObjects
{
    public class OrderBusinessContext
    {
        CartDatabaseContext cartDataBaseContext = new CartDatabaseContext();
        OrderDataBaseContext orderDataBaseContext = new OrderDataBaseContext();
        AddressDatabaseContext addressDatabaseContext = new AddressDatabaseContext();
        ProductDatabaseContext productDatabaseContext = new ProductDatabaseContext();

        CartBusinessContext cartBusinessContext = new CartBusinessContext();

        public bool PlaceOrder(Guid UserID, AddressDTO addressDTO)
        {
            
            Guid AddressID = addressDatabaseContext.AddAddress(UserID, addressDTO);
            CartsDTO cartsDTO = cartDataBaseContext.GetCart(UserID);
            int subtotal = new int();
            foreach (var cartVariant in cartsDTO.CartItems)
            {
                int OrderQuantity = cartVariant.OrderQuantity;
                int Discount = cartVariant.Variant.Product.Discount;
                int Price = cartVariant.Variant.Product.ListingPrice;
                cartVariant.Variant.Product.DiscountedPrice = (Price * (100 - Discount) / 100);
                int DiscountedPrice = cartVariant.Variant.Product.DiscountedPrice;
                subtotal += DiscountedPrice * OrderQuantity;
            }
            cartsDTO.SubTotal = subtotal;
            orderDataBaseContext.PlaceOrder(UserID, cartsDTO, AddressID);
            productDatabaseContext.UpdateInventory(cartsDTO);
            cartBusinessContext.EmptyCart(UserID);
            return true;

        }

        public OrdersDTO GetOrders(Guid UserID)
        {
            OrdersDTO newOrdersDTO = orderDataBaseContext.GetOrders(UserID);
            if(newOrdersDTO.Orders.ToList().Count == 0)
            {
                throw new NoOrderException();
            }
            else
            {
                return newOrdersDTO;
            }
        }
    }
}
