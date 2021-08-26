using System;

namespace ScooterRental.Exceptions
{
    public class EndTimeBeforeStartTimeException : Exception
    {
        public EndTimeBeforeStartTimeException()
        {
        }

        public EndTimeBeforeStartTimeException(string message)
            : base(message)
        {
        }

        public EndTimeBeforeStartTimeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}