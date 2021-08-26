using System;
using System.Collections.Generic;
using System.Linq;

namespace ScooterRental
{
    public class ScooterRentalCompany : IRentalCompany
    {
        private string _name;
        private IDateTimeProvider _dateTimeProvider;
        private IScooterService _scooterService;
        private IRentalService _rentalService;
        public string Name => _name;

        public ScooterRentalCompany(
            string name,
            IDateTimeProvider dateTimeProvider,
            IScooterService scooterService,
            IRentalService rentalService)
        {
            _name = name;
            _dateTimeProvider = dateTimeProvider;
            _scooterService = scooterService;
            _rentalService = rentalService;
        }

        public void StartRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            _rentalService.RentScooter(scooter, _dateTimeProvider.DateTimeNow);
        }

        public decimal EndRent(string id)
        {
            var scooter = _scooterService.GetScooterById(id);
            int rentalDuration = _rentalService.ReturnScooter(scooter, _dateTimeProvider.DateTimeNow);
            return rentalDuration * scooter.PricePerMinute;
        }

        public decimal CalculateIncome(int? year, bool includeNotCompletedRentals)
        {

            decimal incomeFromActiveRentals = 0;
            if (includeNotCompletedRentals)
            {
                incomeFromActiveRentals = _rentalService.CurrentActiveRentals()
                                 .Aggregate(0m, (income, entry) =>
                                      income + (decimal)entry.RentalDuration(_dateTimeProvider.DateTimeNow).TotalMinutes * entry.PricePerMinute);
            }

            decimal incomeFromCompleteRentals = 0;
            incomeFromCompleteRentals = _rentalService.RentalHistory(year)
                .Aggregate(0m, (income, entry) =>
                    income + (decimal)entry.RentalDuration().TotalMinutes * entry.PricePerMinute);

            return incomeFromActiveRentals + incomeFromCompleteRentals;
        }
    }
}