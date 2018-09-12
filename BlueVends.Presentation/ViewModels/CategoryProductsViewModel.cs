using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class CategoryProductsViewModel
    {
        public string Name { get; set; }
        public bool IsLoggedIn { get; set; }
        public IEnumerable<ProductViewModel> Products;
    }
}