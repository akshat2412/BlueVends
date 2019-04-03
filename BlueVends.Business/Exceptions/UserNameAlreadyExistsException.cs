using System;

namespace BlueVends.Business.Exceptions
{
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException() { }
        public UserNameAlreadyExistsException(string message) : base(message) { }
        public UserNameAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
