using System;
using System.Collections.Generic;

namespace ScooterRental
{
    public interface IRentalService
    {
        void RentScooter(Scooter scooter, DateTime time);
        int ReturnScooter(Scooter scooter, DateTime time);
        IList<RentalTime> RentalHistory(int? year);
        IList<RentalTime> CurrentActiveRentals();
    }
}