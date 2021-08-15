using System;
using System.Collections.Generic;

namespace ScooterRental
{
    public class ScooterRentalCompany : IRentalCompany, ITimeService
    {
        private string _name;
        private IScooterService _fleet;
        private IRentalService _rentalService;
        private ITimeService _time;
        public string Name => _name;

        public ScooterRentalCompany(string name)
        {
            _name = name;
        }

        public void StartRent(string id)
        {
            throw new System.NotImplementedException();
        }

        public decimal EndRent(string id)
        {
            throw new System.NotImplementedException();
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new System.NotImplementedException();
        }
        public DateTime Now(DateTime time)
        {
            return DateTime.Now;
        }
    }
}