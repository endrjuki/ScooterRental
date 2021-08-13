using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using Xunit;

namespace ScooterRental.Test
{
    public class ScooterRental
    {
        private ScooterService _testScooterService;
        
        public ScooterRental()
        {
            _testScooterService = new ScooterService();
        }
        
        [Fact]
        public void AddScooter_IdPricePerMinute_Successful()
        {
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;
            
            _testScooterService.AddScooter(id, pricePerMinute);
            var actual = _testScooterService.GetScooterById(id);
            
            Assert.Equal(id, actual.Id);
            Assert.Equal(pricePerMinute, actual.PricePerMinute);
        }

        [Fact]
        public void RemoveScooter_ValidId_RemovesScooterFromFleet()
        {
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;
            var expectedInitialScooterCount = 1;
            var expectedPostScooterCount = 0;
            
            _testScooterService.AddScooter(id, pricePerMinute);
            var actualInitialScooterCount = _testScooterService.GetScooters().Count;
            _testScooterService.RemoveScooter(id);
            var actualPostScooterCount = _testScooterService.GetScooters().Count;
            var actualScooterId = _testScooterService.GetScooters().Count;
            
            Assert.Equal(expectedInitialScooterCount, actualInitialScooterCount);
            Assert.Equal(expectedPostScooterCount, actualPostScooterCount);
        }
        
       [Fact]
        public void RemoveScooter_ScooterWithIdIsRented_ArgumentError()
     
        {
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;
            
            _testScooterService.AddScooter(id, pricePerMinute);
            _testScooterService.GetScooterById(id).IsRented = true;
            Action act = () => _testScooterService.RemoveScooter(id);

            RentInProgressException exception = Assert.Throws<RentInProgressException>(act);
            Assert.Equal("Scooter with this ID is being currently rented.", exception.Message);
        }

        [Fact]
        public void RemoveScooter_ScooterDoesntExist_ArgumentError()
        {
            string id = "testScooter";

            Action act = () => _testScooterService.RemoveScooter(id);
            
            ScooterDoesntExistException exception = Assert.Throws<ScooterDoesntExistException>(act);
            Assert.Equal("ID does not exist in fleet", exception.Message);
        }

        [Fact]
        public void GetScooters_Add3Scooters_ReturnsListOf3Scooters()
        {
            string id1 = "testScooter1";
            string id2 = "testScooter2";
            string id3 = "testScooter3";
            decimal pricePerMinute = 0.20m;
            var expected = new List<Scooter>()
            {
                new Scooter(id1, pricePerMinute),
                new Scooter(id2, pricePerMinute),
                new Scooter(id3, pricePerMinute)
            };
            
            _testScooterService.AddScooter(id1, pricePerMinute);
            _testScooterService.AddScooter(id2, pricePerMinute);
            _testScooterService.AddScooter(id3, pricePerMinute);
            List<Scooter> actual = _testScooterService.GetScooters().ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetScooterById_ValidId_ReturnScooter()
        {
            string id = "testScooter1";
            decimal pricePerMinute = 0.20m;
            var expected = new Scooter(id, pricePerMinute);
            
            _testScooterService.AddScooter(id, pricePerMinute);
            var actual = _testScooterService.GetScooterById(id);
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetScooterById_InvalidId_ArgumentException()
        {
            string id = "testScooter";

            Action act = () => _testScooterService.GetScooterById(id);

            ScooterDoesntExistException exception = Assert.Throws<ScooterDoesntExistException>(act);
            Assert.Equal("ID does not exist in fleet", exception.Message);
        }
    }
}