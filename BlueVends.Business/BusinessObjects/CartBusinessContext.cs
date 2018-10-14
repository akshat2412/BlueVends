using BlueVends.Business.Exceptions;
using BlueVends.DataAccess.DBObjects;
using BlueVends.DataAccess.Exceptions;
using BlueVends.Shared.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Business.BusinessObjects
{
    public class CartBusinessContext
    {
        CartDatabaseContext cartDataBaseContext = new CartDatabaseContext();
        public void AddItemToCart(CartDTO cartDTO)
        {
            try
            {
                bool alreadyPresent = cartDataBaseContext.PresentInCart(cartDTO);
            }
            catch(AlreadyPresentException ex)
            {
                throw new ItemAlreadyInCartException();
            }

            cartDataBaseContext.AddToCart(cartDTO);
        }


        public CartsDTO GetCart(Guid UserID)
        {
            CartsDTO newCartsDTO = cartDataBaseContext.GetCart(UserID);
            int subtotal = new int();
            foreach(var cartVariant in newCartsDTO.CartItems)
            {
                int OrderQuantity = cartVariant.OrderQuantity;
                int Discount = cartVariant.Variant.Product.Discount;
                int Price = cartVariant.Variant.Product.ListingPrice;
                cartVariant.Variant.Product.DiscountedPrice = (Price * (100 - Discount) / 100);
                int DiscountedPrice = cartVariant.Variant.Product.DiscountedPrice;
                subtotal += DiscountedPrice * OrderQuantity;
            }
            newCartsDTO.SubTotal = subtotal;
            return newCartsDTO;
        }

        public bool EmptyCart(Guid UserID)
        {
            cartDataBaseContext.EmptyCart(UserID);
            return true;
        }

        public void RemoveItem(Guid UserID, Guid VariantID)
        {
            cartDataBaseContext.RemoveItem(UserID, VariantID);
        }
    }

    
}
