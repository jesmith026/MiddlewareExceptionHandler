using System;

namespace MiddlewareExceptionHandler.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string msg)
            : base(msg) { }

        public NotFoundException(string msg, Exception exception)
            : base(msg, exception) { }
    }
}