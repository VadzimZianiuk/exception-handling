using System;

namespace Task3
{
    public class UserNotFoundException : ArgumentException
    {
        public UserNotFoundException(string message)
            : base(message)
        {
        }

        public UserNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserNotFoundException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public UserNotFoundException(string message, string paramName)
            : base(message, paramName)
        {
        }
    }
}
