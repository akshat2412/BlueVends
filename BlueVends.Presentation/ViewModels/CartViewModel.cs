using BlueVends.Presentation.Custom_Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class CartViewModel
    {
        [ValidateQuantity]
        public int OrderQuantity { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid VariantID { get; set; }
        public System.Guid ProductID { get; set; }
        public int OrderLimit { get; set; }
        public int Inventory { get; set; }
    }
}