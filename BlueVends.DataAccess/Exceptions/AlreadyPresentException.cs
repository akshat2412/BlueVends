using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueVends.DataAccess.Exceptions
{
    public class AlreadyPresentException : Exception
    {
        public AlreadyPresentException() { }
        public AlreadyPresentException(string message) : base(message) { }
        public AlreadyPresentException(string message, Exception inner) : base(message, inner) { }
    }
}
