using System;

namespace ScooterRental.Exceptions
{
    public class RentalEntryDoesntExistException : Exception
    {
        public RentalEntryDoesntExistException()
        {
        }

        public RentalEntryDoesntExistException(string message)
            : base(message)
        {
        }

        public RentalEntryDoesntExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}