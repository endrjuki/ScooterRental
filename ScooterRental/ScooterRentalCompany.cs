using System.Collections.Generic;

namespace ScooterRental
{
    public class ScooterRentalCompany : IRentalCompany, IScooterService
    {
        private string _name;
        private List<Scooter> _fleet;
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

        public void AddScooter(string id, decimal pricePerMinute)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveScooter(string id)
        {
            throw new System.NotImplementedException();
        }

        public IList<Scooter> GetScooters()
        {
            throw new System.NotImplementedException();
        }

        public Scooter GetScooterById(string scooterId)
        {
            throw new System.NotImplementedException();
        }
    }
}