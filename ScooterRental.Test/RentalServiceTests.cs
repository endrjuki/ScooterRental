using System;
using Xunit;

namespace ScooterRental.Test
{
    public class RentalServiceTests
    {
        private RentalService _testService;
        
        RentalServiceTests()
        {
            _testService = new RentalService();
        }
        
        [Fact]
        public void RentScooter_RentOneScooter_ScooterAddedToCurrentActiveRentals()
        {
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            var testScooter = new Scooter("testName", 0.20m);
            
            _testService.RentScooter(testScooter, testTimeNow);
            var actualRental = _testService.CurrentActiveRentals()[0];
            var actualID = actualRental.Id;
            var actualCpm = actualRental.PricePerMinute;
            var actualRentStartTime = actualRental.StartTime;
            
            Assert.Equal(testScooter.Id, actualID);
            Assert.Equal(testScooter.PricePerMinute, actualCpm);
            Assert.Equal(testTimeNow, actualRentStartTime);
        }

        [Fact]
        public void ReturnScooter_ReturnOneScoot_ScooterRemovedFromCurrentActiveRentals_AddedToHistory()
        {
            var rentalStartTime = new DateTime(2021, 8, 13, 0, 0, 0);
            
        }
        
    }
}