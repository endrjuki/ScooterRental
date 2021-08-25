using System;
using System.Collections.Generic;
using System.Linq;
using ScooterRental.Exceptions;
using Xunit;

namespace ScooterRental.Test
{
    public class ScooterRental
    {
        private ScooterService _sut;

        public ScooterRental()
        {
            _sut = new ScooterService();
        }

        [Fact]
        public void AddScooter_IdPricePerMinute_Successful()
        {
            // Arrange
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;

            // Act
            _sut.AddScooter(id, pricePerMinute);
            var actual = _sut.GetScooterById(id);

            // Assert
            Assert.Equal(id, actual.Id);
            Assert.Equal(pricePerMinute, actual.PricePerMinute);
        }

        [Fact]
        public void AddScooter_NegativePricePerMinute_NegativePricePerMinuteException()
        {
            // Arrange
            string id = "testScooter";
            decimal pricePerMinute = -0.22m;
            var expectedMessage = "Price per minute must be larger than 0.";

            // Act
            Action act = () => _sut.AddScooter(id, pricePerMinute);

            // Assert
            NegativePricePerMinuteException exception = Assert.Throws<NegativePricePerMinuteException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void RemoveScooter_ValidId_RemovesScooterFromFleet()
        {
            // Arrange
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;
            var expectedInitialScooterCount = 1;
            var expectedPostScooterCount = 0;

            // Act
            _sut.AddScooter(id, pricePerMinute);
            var actualInitialScooterCount = _sut.GetScooters().Count;
            _sut.RemoveScooter(id);
            var actualPostScooterCount = _sut.GetScooters().Count;
            var actualScooterId = _sut.GetScooters().Count;

            // Assert
            Assert.Equal(expectedInitialScooterCount, actualInitialScooterCount);
            Assert.Equal(expectedPostScooterCount, actualPostScooterCount);
        }

       [Fact]
        public void RemoveScooter_ScooterWithIdIsRented_RentInProgressException()
        {
            // Arrange
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;

            // Act
            _sut.AddScooter(id, pricePerMinute);
            _sut.GetScooterById(id).IsRented = true;
            Action act = () => _sut.RemoveScooter(id);

            // Assert
            RentInProgressException exception = Assert.Throws<RentInProgressException>(act);
            Assert.Equal("Scooter with this ID is being currently rented.", exception.Message);
        }

        [Fact]
        public void RemoveScooter_ScooterDoesntExist_ScooterDoesntExist()
        {
            // Arrange
            string id = "testScooter";

            // Act
            Action act = () => _sut.RemoveScooter(id);

            // Assert
            ScooterDoesntExistException exception = Assert.Throws<ScooterDoesntExistException>(act);
            Assert.Equal("ID does not exist in fleet", exception.Message);
        }

        [Fact]
        public void GetScooters_Add3Scooters_ReturnsListOf3Scooters()
        {
            // Arrange
            string id1 = "testScooter1";
            string id2 = "testScooter2";
            string id3 = "testScooter3";
            decimal pricePerMinute = 0.20m;
            var expected = 3;

            // Act
            _sut.AddScooter(id1, pricePerMinute);
            _sut.AddScooter(id2, pricePerMinute);
            _sut.AddScooter(id3, pricePerMinute);
            int actual = _sut.GetScooters().Count;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetScooterById_ValidId_ReturnScooter()
        {
            // Arrange
            string id = "testScooter1";
            decimal pricePerMinute = 0.20m;
            var expected = new Scooter(id, pricePerMinute);

            // Act
            _sut.AddScooter(id, pricePerMinute);
            var actual = _sut.GetScooterById(id);

            // Assert
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.IsRented, actual.IsRented);
            Assert.Equal(expected.PricePerMinute, actual.PricePerMinute);
        }

        [Fact]
        public void GetScooterById_InvalidId_ScooterDoesntExist()
        {
            // Arrange
            string id = "testScooter";

            // Act
            Action act = () => _sut.GetScooterById(id);

            // Assert
            ScooterDoesntExistException exception = Assert.Throws<ScooterDoesntExistException>(act);
            Assert.Equal("ID does not exist in fleet", exception.Message);
        }
    }
}