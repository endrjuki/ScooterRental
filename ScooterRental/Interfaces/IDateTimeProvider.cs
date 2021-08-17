using System;
using System.Diagnostics.CodeAnalysis;

namespace ScooterRental
{
    public interface IDateTimeProvider
    {
        public DateTime DateTimeNow { get; }
    }
}