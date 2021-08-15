using System;
using System.Collections.Generic;
using ScooterRental.Exceptions;
using Xunit;
using Moq;

namespace ScooterRental.Test
{
    
    public class ScooterRentalCompanyTests : ITimeService
    {
        private IScooterService _mockScooterService;
        ScooterRentalCompanyTests()
        {
            _mockScooterService = new MockScooterService();
        }
        
        [Fact]
        public void StartRent_()
        {
            string scooterId = "1";
            var scooter = _mockScooterService.GetScooterById(scooterId);

        }
        
        [Fact]
        public void EndRent()
        {
            
        }

        [Fact]
        public void CalculateIncome()
        {
            
        }

        public DateTime Now(DateTime time)
        {
            return time;
        }
    }
    
    public class MockScooterService : IScooterService
    {
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
            return new Scooter(scooterId, 0.2m);
        }
    }
}