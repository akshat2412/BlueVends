using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Business.Exceptions
{
    class ProductDoesNotExistsException : Exception
    {
        public ProductDoesNotExistsException() { }
        public ProductDoesNotExistsException(string message) : base(message) { }
        public ProductDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
