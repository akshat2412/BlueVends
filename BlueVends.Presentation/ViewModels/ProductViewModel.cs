using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class ProductViewModel
    {
        public bool IsLoggedIn { get; set; }
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public int ListingPrice { get; set; }
        public int Inventory { get; set; }
        public int QuantitySold { get; set; }
        public int OrderLimit { get; set; }
        public string ImageURL { get; set; }
        public System.Guid CategoryID { get; set; }
        public IEnumerable<VariantViewModel> Variants { get; set; }
        public int DiscountedPrice { get; set; }
    }
}