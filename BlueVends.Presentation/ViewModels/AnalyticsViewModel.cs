using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class AnalyticsViewModel
    {
        public IEnumerable<CategoryProductsViewModel> Categories { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}