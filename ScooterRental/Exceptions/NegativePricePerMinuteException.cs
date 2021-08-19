using System;

namespace ScooterRental.Test
{
    public class NegativePricePerMinuteException : Exception
    {
        public NegativePricePerMinuteException()
        {
        }

        public NegativePricePerMinuteException(string message)
            : base(message)
        {
        }

        public NegativePricePerMinuteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}