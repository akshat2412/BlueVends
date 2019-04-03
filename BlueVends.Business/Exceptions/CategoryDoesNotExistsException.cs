using System;

namespace BlueVends.Business.Exceptions
{
    public class CategoryDoesNotExistsException : Exception
    {
        public CategoryDoesNotExistsException() { }
        public CategoryDoesNotExistsException(string message) : base(message) { }
        public CategoryDoesNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
