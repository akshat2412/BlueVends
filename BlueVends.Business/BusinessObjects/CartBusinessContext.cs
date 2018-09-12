using BlueVends.Business.Exceptions;
using BlueVends.DataAccess.DBObjects;
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
        public void AddItemToCart(Guid UserID, Guid VariantID)
        {
            try
            {
                bool alreadyPresent = cartDataBaseContext.PresentInCart(UserID, VariantID);
            }
            catch(Exception ex)
            {
                throw new ItemAlreadyInCartException();
            }

            try
            {
                cartDataBaseContext.AddToCart(UserID, VariantID);
            }
            catch
            {
                throw new Exception("Unexpected Error");
            }
        }
    }
}
