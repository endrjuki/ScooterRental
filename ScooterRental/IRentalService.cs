using System;
using System.Collections.Generic;

namespace ScooterRental
{
    public interface IRentalService
    {
        void RentScooter(Scooter scooter, DateTime time);
        void ReturnScooter(Scooter scooter, DateTime time);
        IList<RentalTime> RentalHistory();
        IList<RentalTime> CurrentActiveRentals();
        decimal CalculateIncomeActiveRentals(int year);
        decimal CalculateIncomePastRentals(int year);
    }
}