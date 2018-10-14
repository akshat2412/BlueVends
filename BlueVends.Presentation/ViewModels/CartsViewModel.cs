using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class CartsViewModel
    {
        public int SubTotal { get; set; }
        public bool IsLoggedIn { get; set; }
        public IEnumerable<CartVariantViewModel> CartItems;
    }
}