using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class OrderProductViewModel
    {
        public string Name { get; set; }
        public int SellingPrice { get; set; }
        public string Variant { get; set; }
        public int Quantity { get; set; }
        public System.Guid ProductID { get; set; }
    }
}