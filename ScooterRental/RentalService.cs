using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentalService : IRentalService
    {
        private List<RentalTime> _completeRentals;
        private List<RentalTime> _activeRentals;

        public RentalService()
        {
            _completeRentals = new List<RentalTime>();
            _activeRentals = new List<RentalTime>();
        }

        public void RentScooter(Scooter scooter, DateTime currentTime)
        {
            if (scooter.IsRented)
            {
                throw new RentInProgressException("Scooter with this ID is being currently rented.");
            }

            _activeRentals.Add(new RentalTime(scooter.Id, scooter.PricePerMinute, currentTime));
        }

        public int ReturnScooter(Scooter scooter, DateTime time)
        {
            var rentalEntry = _activeRentals.Find(entry => entry.Id == scooter.Id);
            if (rentalEntry is null)
            {
                throw new RentalEntryDoesntExistException("Rental entry with this ID doesn't exist");
            }

            rentalEntry.End(time);
            _activeRentals.RemoveAll(entry => entry.Id == rentalEntry.Id);
            _completeRentals.Add(rentalEntry);

            return rentalEntry.RentalDuration(time).Minutes;
        }

        public IList<RentalTime> RentalHistory(int? year)
        {
            if (year is null)
            {
                return _completeRentals.ToList();
            }
            return _completeRentals.Where(entry => entry.EndTime.Year == year).ToList();
        }

        public IList<RentalTime> CurrentActiveRentals()
        {
            return _activeRentals.ToList();
        }
    }
}