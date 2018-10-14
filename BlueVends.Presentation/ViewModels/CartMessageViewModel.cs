using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueVends.Presentation.ViewModels
{
    public class CartMessageViewModel
    {
        public string SuccessMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
        public bool IsLoggedIn { get; set; }
        public CartMessageViewModel()
        {
            ErrorMessages = new List<string>();
        }
    }
}