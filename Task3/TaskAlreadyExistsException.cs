using System;

namespace Task3
{
    internal class TaskAlreadyExistsException : ArgumentException
    {
        public TaskAlreadyExistsException(string message)
            : base(message)
        {
        }

        public TaskAlreadyExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TaskAlreadyExistsException(string message, string paramName, Exception innerException)
            : base(message, paramName, innerException)
        {
        }

        public TaskAlreadyExistsException(string message, string paramName)
            : base(message, paramName)
        {
        }
    }
}
