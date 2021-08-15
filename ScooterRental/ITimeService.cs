using System;
using System.Diagnostics.CodeAnalysis;

namespace ScooterRental
{
    public interface ITimeService
    {
        DateTime Now(DateTime time);

    }
}