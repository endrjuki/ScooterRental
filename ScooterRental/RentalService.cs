using System;
using System.Collections.Generic;

namespace ScooterRental
{
    public class RentalService : IRentalService
    {
        private List<RentalTime> _rentalHistory;
        private List<RentalTime> _currentlyActiveRentals;
        
        public void RentScooter(Scooter scooter, DateTime time)
        {
            throw new NotImplementedException();
        }

        public void ReturnScooter(Scooter scooter, DateTime time)
        {
            throw new NotImplementedException();
        }

        public IList<RentalTime> RentalHistory(int year)
        {
            throw new NotImplementedException();
        }

        public IList<RentalTime> CurrentActiveRentals()
        {
            throw new NotImplementedException();
        }

        public decimal CalculateIncomePastRentals(int year)
        {
            throw new NotImplementedException();
        }
    }
}