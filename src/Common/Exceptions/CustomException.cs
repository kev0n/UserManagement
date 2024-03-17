using System;

namespace Common.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message)
            : base(message) { }

        public CustomException(string message, Exception inner)
            : base(message, inner) { }
    }
}