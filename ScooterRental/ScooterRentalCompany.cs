using System;
using System.Collections.Generic;

namespace ScooterRental
{
    public class ScooterRentalCompany : IRentalCompany
    {
        private string _name;
        private Dictionary<string, DateTime> _rentalLog;
        public string Name => _name;

        public ScooterRentalCompany(string name)
        {
            _name = name;
        }

        public void StartRent(string id)
        {
            throw new System.NotImplementedException();
        }
        
        public void StartRent(string id, DateTime timeNow)
        {
            throw new System.NotImplementedException();
        }

        public decimal EndRent(string id)
        {
            throw new System.NotImplementedException();
        }
        
        public decimal EndRent(string id, DateTime timeNow)
        {
            throw new System.NotImplementedException();
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {
            throw new System.NotImplementedException();
        }

        

        

        
    }
}