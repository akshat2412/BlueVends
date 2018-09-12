using BlueVends.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.DataAccess.DBObjects
{
    public class CartDatabaseContext
    {
        BlueVendsDBEntities dbContext = new BlueVendsDBEntities();
        public bool PresentInCart(Guid UserID, Guid VariantID)
        {
            IEnumerable<Cart> carts = dbContext.Cart.Where(p => p.UserID== UserID);
            foreach(var cart in carts)
            {
                if(cart.VariantID == VariantID)
                {
                    throw new Exception("Item already in cart");
                }
            }
            return false;
        }

        async public void AddToCart(Guid UserID, Guid VariantID)
        {
            Cart cart = new Cart { UserID = UserID, VariantID = VariantID };
            dbContext.Cart.Add(cart);
            await dbContext.SaveChangesAsync();
        }
    }
}
