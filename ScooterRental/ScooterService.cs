using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using ScooterRental.Test;

namespace ScooterRental
{
    public class ScooterService : IScooterService
    {
        private List<Scooter> _fleet;
        public ScooterService()
        {
            _fleet = new List<Scooter>();
        }

        public void AddScooter(string id, decimal pricePerMinute)
        {
            if (pricePerMinute <= 0)
            {
                throw new NegativePricePerMinuteException("Price per minute must be larger than 0.");
            }
            _fleet.Add(new Scooter(id, pricePerMinute));
        }

        public void RemoveScooter(string id)
        {
            var scooter = _fleet.Find(scooter => scooter.Id == id);

            if (scooter is null)
            {
                throw new ScooterDoesntExistException("ID does not exist in fleet");
            }

            if (scooter.IsRented)
            {
                throw new RentInProgressException("Scooter with this ID is being currently rented.");
            }

            _fleet.Remove(scooter);
        }

        public IList<Scooter> GetScooters()
        {
            return _fleet.ToList();
        }

        public Scooter GetScooterById(string scooterId)
        {   var scooter = _fleet.Find(scooter => scooter.Id == scooterId);

            if (scooter is null)
            {
                throw new ScooterDoesntExistException("ID does not exist in fleet");
            }

            return scooter;
        }
    }
}