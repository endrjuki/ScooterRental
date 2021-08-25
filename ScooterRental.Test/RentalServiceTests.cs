using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using ScooterRental.Exceptions;
using Xunit;

namespace ScooterRental.Test
{
    public class RentalServiceTests
    {
        private RentalService _sut;
        private Scooter _mockScooter;

        public RentalServiceTests()
        {
            _sut = new RentalService();
            _mockScooter = new Mock<Scooter>("testName", 0.20m).Object;
        }

        [Fact]
        public void RentScooter_ValidScooter_ScooterAddedToCurrentActiveRentals()
        {
            // Arrange
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            var expectedRentalCount = 1;

            // Act
            _sut.RentScooter(_mockScooter, testTimeNow);
            var actualRentalCount = _sut.CurrentActiveRentals().Count;
            var actualRentalScooter = _sut.CurrentActiveRentals()[0];
            var actualId = actualRentalScooter.Id;
            var actualPpm = actualRentalScooter.PricePerMinute;
            var actualRentStartTime = actualRentalScooter.StartTime;

            // Assert
            Assert.Equal(expectedRentalCount, actualRentalCount);
        }

        [Fact]
        public void RentScooter_ScooterThatIsRented_RentInProgressException()
        {
            // Arrange
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            _mockScooter.IsRented = true;
            var expectedMessage = "Scooter with this ID is being currently rented.";

            // Act
            Action act = () => _sut.RentScooter(_mockScooter, testTimeNow);

            // Assert
            RentInProgressException exception = Assert.Throws<RentInProgressException>(act);
            Assert.Equal(exception.Message, expectedMessage);
        }

        [Fact]
        public void ReturnScooter_RentalEntryDoesntExist_RentalEntryDoesntExistException()
        {
            // Arrange
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            var expectedMessage = "Rental entry with this ID doesn't exist";

            // Act
            Action act = () => _sut.ReturnScooter(_mockScooter, testTimeNow);

            // Assert
            RentalEntryDoesntExistException exception = Assert.Throws<RentalEntryDoesntExistException>(act);
            Assert.Equal(exception.Message, expectedMessage);
        }

        [Fact]
        public void ReturnScooter_RemovedFromCurrentActiveRentals_AddedToHistory()
        {
            // Arrange
            var rentalYear = 2021;
            var rentalStartTime = new DateTime(rentalYear, 8, 13, 0, 0, 0);
            var rentalEndTime = new DateTime(rentalYear, 8, 13, 2, 0, 0);
            var expectedRentalHistoryCount = 1;
            _sut.RentScooter(_mockScooter, rentalStartTime);

            // Act
            _sut.ReturnScooter(_mockScooter, rentalEndTime);
            var actualRentalHistoryCount = _sut.RentalHistory(rentalYear).Count;
            var actualRentalTime = _sut.RentalHistory(rentalYear)[0];
            var actualId = actualRentalTime.Id;
            var actualStartTime = actualRentalTime.StartTime;
            var actualEndTime = actualRentalTime.EndTime;

            // Assert
            Assert.Equal(expectedRentalHistoryCount, actualRentalHistoryCount);
            Assert.Equal(_mockScooter.Id, actualId);
            Assert.Equal(rentalStartTime, actualStartTime);
            Assert.Equal(rentalEndTime, actualEndTime);
        }

        [Fact]
        public void ReturnScooter_DoesntExistInActiveRentals_RentalEntryDoesntExistException()
        {
            // Arrange
            var rentalYear = 2021;
            var rentalEndTime = new DateTime(rentalYear, 8, 13, 2, 0, 0);
            var expectedMessage = "Rental entry with this ID doesn't exist";

            // Act
            Action act = () => _sut.ReturnScooter(_mockScooter, rentalEndTime);

            // Assert
            RentalEntryDoesntExistException exception = Assert.Throws<RentalEntryDoesntExistException>(act);
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void RentalHistory_NoYearGiven_ReturnListOfAllRentalEntries()
        {
            // Arrange
            var rentalYear1 = 2019;
            var rentalYear2 = 2020;
            var rentalYear3 = 2021;
            var timeYear1 = new DateTime(rentalYear1, 1, 1);
            var timeYear2 = new DateTime(rentalYear2, 1, 1);
            var timeYear3 = new DateTime(rentalYear1, 1, 1);
            var timeDelta = new TimeSpan(0, 40, 0);
            var expectedScooterCount = 3;

            // Act
            _sut.RentScooter(_mockScooter, timeYear1);
            _sut.ReturnScooter(_mockScooter, timeYear1 + timeDelta);
            _sut.RentScooter(_mockScooter, timeYear2);
            _sut.ReturnScooter(_mockScooter, timeYear2 + timeDelta);
            _sut.RentScooter(_mockScooter, timeYear3);
            _sut.ReturnScooter(_mockScooter, timeYear3 + timeDelta);
            var actualScooterCount = _sut.RentalHistory().Count;

            // Assert
            Assert.Equal(expectedScooterCount, actualScooterCount);
        }

        [Fact]
        public void RentalHistory_Year2019_ReturnOneEntry()
        {
            // Arrange
            var rentalYear1 = 2019;
            var rentalYear2 = 2020;
            var rentalYear3 = 2021;
            var timeYear1 = new DateTime(rentalYear1, 1, 1);
            var timeYear2 = new DateTime(rentalYear2, 1, 1);
            var timeYear3 = new DateTime(rentalYear3, 1, 1);
            var timeDelta = new TimeSpan(0, 40, 0);
            var expectedEntryCount = 1;

            // Act
            _sut.RentScooter(_mockScooter, timeYear1);
            _sut.ReturnScooter(_mockScooter, timeYear1 + timeDelta);
            _sut.RentScooter(_mockScooter, timeYear2);
            _sut.ReturnScooter(_mockScooter, timeYear2 + timeDelta);
            _sut.RentScooter(_mockScooter, timeYear3);
            _sut.ReturnScooter(_mockScooter, timeYear3 + timeDelta);
            var actualEntryCount = _sut.RentalHistory(rentalYear1).Count;
            var actualEntryYear = _sut.RentalHistory(rentalYear1)[0].EndTime.Year;

            // Assert
            Assert.Equal(expectedEntryCount, actualEntryCount);
            Assert.Equal(rentalYear1, actualEntryYear);
        }
    }
}