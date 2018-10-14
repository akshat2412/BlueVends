using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.Business.Exceptions
{
    public class NoOrderException : Exception
    {
        public NoOrderException() { }
        public NoOrderException(string message) : base(message) { }
        public NoOrderException(string message, Exception inner) : base(message, inner) { }
    }
}
