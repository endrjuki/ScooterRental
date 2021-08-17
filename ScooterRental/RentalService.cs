using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentalService : IRentalService
    {
        private List<RentalTime> _rentalHistory;
        private List<RentalTime> _currentlyActiveRentals;

        public RentalService()
        {
            _rentalHistory = new List<RentalTime>();
            _currentlyActiveRentals = new List<RentalTime>();
        }

        public void RentScooter(Scooter scooter, DateTime currentTime)
        {
            if (scooter.IsRented)
            {
                throw new RentInProgressException("Scooter with this ID is being currently rented.");
            }

            _currentlyActiveRentals.Add(new RentalTime(scooter.Id, scooter.PricePerMinute, currentTime));
        }

        public int ReturnScooter(Scooter scooter, DateTime time)
        {
            var rentalEntry = _currentlyActiveRentals.Find(entry => entry.Id == scooter.Id);
            if (rentalEntry is null)
            {
                throw new RentalEntryDoesntExistException("Rental entry with this ID doesn't exist");
            }

            rentalEntry.End(time);
            _currentlyActiveRentals.RemoveAll(entry => entry.Id == rentalEntry.Id);
            _rentalHistory.Add(rentalEntry);

            return rentalEntry.RentalDuration(time).Minutes;
        }

        public IList<RentalTime> RentalHistory(int? year)
        {
            if (year is null)
            {
                return _rentalHistory;
            }
            return _rentalHistory.Where(entry => entry.EndTime.Year == year).ToList();
        }

        public IList<RentalTime> CurrentActiveRentals()
        {
            return _currentlyActiveRentals;
        }
    }
}