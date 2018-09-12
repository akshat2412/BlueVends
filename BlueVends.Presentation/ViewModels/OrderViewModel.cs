using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class OrderViewModel
    {
        public string Status { get; set; }
        public int TotalDue { get; set; }
        public string Address { get; set; }
        public System.DateTime OrderDate { get; set; }
        public IEnumerable<OrderProductViewModel> Products { get; set; }
    }
}