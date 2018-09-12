using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.DataAccess.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() { }
        public ProductNotFoundException(string message) : base(message) { }
        public ProductNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
