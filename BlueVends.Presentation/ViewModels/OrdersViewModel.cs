using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<OrderViewModel> Orders;
        public bool IsLoggedIn { get; set; }
    }
}