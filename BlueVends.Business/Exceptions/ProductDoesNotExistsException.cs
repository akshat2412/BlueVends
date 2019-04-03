using System;

namespace BlueVends.Business.Exceptions
{
    class ProductDoesNotExistsException : Exception
    {
        public ProductDoesNotExistsException() { }
        public ProductDoesNotExistsException(string message) : base(message) { }
        public ProductDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
