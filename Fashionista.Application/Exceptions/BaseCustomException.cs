namespace Fashionista.Application.Exceptions
{
    using System;

    public class BaseCustomException : Exception
    {
        public BaseCustomException()
        {
        }

        public BaseCustomException(string message)
            : base(message)
        {
        }
    }
}