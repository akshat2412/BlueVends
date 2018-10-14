using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class CartVariantViewModel
    {
        public string Name;
        public VariantViewModel Variant;
        public Guid ID;
        public int OrderQuantity;
    }
}