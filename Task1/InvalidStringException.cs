using System;

namespace Task1
{
    internal class InvalidStringException : ArgumentException
    {
        public InvalidStringException(string message)
            : base(message)
        {
        }

        public InvalidStringException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidStringException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public InvalidStringException(string message, string paramName)
            : base(message, paramName)
        {
        }
    }
}
