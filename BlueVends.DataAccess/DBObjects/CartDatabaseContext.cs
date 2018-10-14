using BlueVends.DataAccess.Exceptions;
using BlueVends.Entities;
using BlueVends.Shared.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper;
using BlueVends.Shared.DTO.Shared;
using BlueVends.Shared.DTO.Product;
using BlueVends.Shared.DTO.Variant;

namespace BlueVends.DataAccess.DBObjects
{
    public class CartDatabaseContext
    {
        BlueVendsDBEntities dbContext = new BlueVendsDBEntities();
        IMapper CartItemsMapper;

        public CartDatabaseContext()
        {
            var cartItemsConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Variant, VariantDTO>();
                cfg.CreateMap<Cart, CartVariantDTO>();
                cfg.CreateMap<Product, ProductDTO>();
            });
            CartItemsMapper = new Mapper(cartItemsConfig);
        }
        public bool PresentInCart(CartDTO cartDTO)
        {
            IEnumerable<Cart> carts = dbContext.Cart.Where(p => p.UserID== cartDTO.UserID);
            foreach(var cart in carts)
            {
                if(cart.VariantID == cartDTO.VariantID)
                {
                    throw new AlreadyPresentException();
                }
            }
            return false;
        }

        public void AddToCart(CartDTO cartDTO)
        {
            Cart cart = new Cart { UserID = cartDTO.UserID, VariantID = cartDTO.VariantID, OrderQuantity = cartDTO.OrderQuantity };
            dbContext.Cart.Add(cart);
            dbContext.SaveChanges();
        }

        public CartsDTO GetCart(Guid UserID)
        {
            var carts = dbContext.Cart.Include(c => c.Variant.Product).Where(c => c.UserID == UserID).ToList();
            CartsDTO cartsDTO = new CartsDTO();
            cartsDTO.CartItems = CartItemsMapper.Map<IEnumerable<Cart>, IEnumerable<CartVariantDTO>>(carts);
            return cartsDTO;
        }

        public void EmptyCart(Guid UserID)
        {
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID));
            dbContext.SaveChanges();
            return;
        }

        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            dbContext.Cart.RemoveRange(dbContext.Cart.Where(c => c.UserID == UserID && c.VariantID == VariantID));
            dbContext.SaveChanges();
            return;
        }
    }
}
