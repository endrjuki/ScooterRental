using System;

namespace ScooterRental
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}