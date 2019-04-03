using System;

namespace BlueVends.Business.Exceptions
{
    public class ItemAlreadyInCartException : Exception
    {
        public ItemAlreadyInCartException() { }
        public ItemAlreadyInCartException(string message) : base(message) { }
        public ItemAlreadyInCartException(string message, Exception inner) : base(message, inner) { }
    }
}
