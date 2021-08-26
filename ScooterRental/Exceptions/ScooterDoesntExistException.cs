using System;

namespace ScooterRental.Exceptions
{
    public class ScooterDoesntExistException : Exception
    {
        public ScooterDoesntExistException()
        {
        }

        public ScooterDoesntExistException(string message)
            : base(message)
        {
        }

        public ScooterDoesntExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}