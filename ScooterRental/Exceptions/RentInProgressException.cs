using System;

namespace ScooterRental.Exceptions
{
    public class RentInProgressException : Exception
    {
        public RentInProgressException()
        {
        }

        public RentInProgressException(string message)
            : base(message)
        {
        }

        public RentInProgressException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}