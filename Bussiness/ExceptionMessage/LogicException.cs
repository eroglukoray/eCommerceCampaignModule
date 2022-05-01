using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.ExceptionMessage
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        {
        }
    }
}
