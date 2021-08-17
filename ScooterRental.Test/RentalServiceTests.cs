using System;
using System.Collections;
using System.Collections.Generic;
using ScooterRental.Exceptions;
using Xunit;

namespace ScooterRental.Test
{
    public class RentalServiceTests
    {
        private RentalService _testService;
        private Scooter _testScooter;

        public RentalServiceTests()
        {
            _testService = new RentalService();
            _testScooter = new Scooter("testName", 0.20m);
        }

        [Fact]
        public void RentScooter_ValidScooter_ScooterAddedToCurrentActiveRentals()
        {
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            var expectedRentalCount = 1;

            _testService.RentScooter(_testScooter, testTimeNow);
            var actualRentalCount = _testService.CurrentActiveRentals().Count;
            var actualRentalScooter = _testService.CurrentActiveRentals()[0];
            var actualId = actualRentalScooter.Id;
            var actualPpm = actualRentalScooter.PricePerMinute;
            var actualRentStartTime = actualRentalScooter.StartTime;

            Assert.Equal(expectedRentalCount, actualRentalCount);
            Assert.Equal(_testScooter.Id, actualId);
            Assert.Equal(_testScooter.PricePerMinute, actualPpm);
            Assert.Equal(testTimeNow, actualRentStartTime);
        }

        [Fact]
        public void RentScooter_ScooterThatIsRented_RentInProgressException()
        {
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            _testScooter.IsRented = true;
            var expectedMessage = "Scooter with this ID is being currently rented.";

            Action act = () => _testService.RentScooter(_testScooter, testTimeNow);

            RentInProgressException exception = Assert.Throws<RentInProgressException>(act);
            Assert.Equal(exception.Message, expectedMessage);
        }

        [Fact]
        public void ReturnScooter_RentalEntryDoesntExist_RentalEntryDoesntExistException()
        {
            var testTimeNow = new DateTime(2021, 8, 13, 0, 0, 0);
            var expectedMessage = "Rental entry with this ID doesn't exist";

            Action act = () => _testService.ReturnScooter(_testScooter, testTimeNow);

            RentalEntryDoesntExistException exception = Assert.Throws<RentalEntryDoesntExistException>(act);
            Assert.Equal(exception.Message, expectedMessage);
        }

        [Fact]
        public void ReturnScooter_RemovedFromCurrentActiveRentals_AddedToHistory()
        {
            var rentalYear = 2021;
            var rentalStartTime = new DateTime(rentalYear, 8, 13, 0, 0, 0);
            var rentalEndTime = new DateTime(rentalYear, 8, 13, 2, 0, 0);
            var expectedRentalHistoryCount = 1;
            _testService.RentScooter(_testScooter, rentalStartTime);

            _testService.ReturnScooter(_testScooter, rentalEndTime);
            var actualRentalHistoryCount = _testService.RentalHistory(rentalYear).Count;
            var actualRentalTime = _testService.RentalHistory(rentalYear)[0];
            var actualId = actualRentalTime.Id;
            var actualStartTime = actualRentalTime.StartTime;
            var actualEndTime = actualRentalTime.EndTime;

            Assert.Equal(expectedRentalHistoryCount, actualRentalHistoryCount);
            Assert.Equal(_testScooter.Id, actualId);
            Assert.Equal(rentalStartTime, actualStartTime);
            Assert.Equal(rentalEndTime, actualEndTime);
        }

        [Fact]
        public void RentalHistory_YearNull_ReturnListOfAllRentalEntries()
        {
            var rentalYear1 = 2019;
            var rentalYear2 = 2020;
            var rentalYear3 = 2021;
            var timeYear1 = new DateTime(rentalYear1, 1, 1);
            var timeYear2 = new DateTime(rentalYear2, 1, 1);
            var timeYear3 = new DateTime(rentalYear1, 1, 1);
            var timeDelta = new TimeSpan(0, 40, 0);
            var expectedScooterCount = 3;

            _testService.RentScooter(_testScooter, timeYear1);
            _testService.ReturnScooter(_testScooter, timeYear1 + timeDelta);
            _testService.RentScooter(_testScooter, timeYear2);
            _testService.ReturnScooter(_testScooter, timeYear2 + timeDelta);
            _testService.RentScooter(_testScooter, timeYear3);
            _testService.ReturnScooter(_testScooter, timeYear3 + timeDelta);
            var actualScooterCount = _testService.RentalHistory(null).Count;

            Assert.Equal(expectedScooterCount, actualScooterCount);
        }

        [Fact]
        public void RentalHistory_Year2019_ReturnOneEntry()
        {
            var rentalYear1 = 2019;
            var rentalYear2 = 2020;
            var rentalYear3 = 2021;
            var timeYear1 = new DateTime(rentalYear1, 1, 1);
            var timeYear2 = new DateTime(rentalYear2, 1, 1);
            var timeYear3 = new DateTime(rentalYear3, 1, 1);
            var timeDelta = new TimeSpan(0, 40, 0);
            var expectedEntryCount = 1;

            _testService.RentScooter(_testScooter, timeYear1);
            _testService.ReturnScooter(_testScooter, timeYear1 + timeDelta);
            _testService.RentScooter(_testScooter, timeYear2);
            _testService.ReturnScooter(_testScooter, timeYear2 + timeDelta);
            _testService.RentScooter(_testScooter, timeYear3);
            _testService.ReturnScooter(_testScooter, timeYear3 + timeDelta);
            var actualEntryCount = _testService.RentalHistory(rentalYear1).Count;
            var actualEntryYear = _testService.RentalHistory(rentalYear1)[0].EndTime.Year;

            Assert.Equal(expectedEntryCount, actualEntryCount);
            Assert.Equal(rentalYear1, actualEntryYear);
        }
    }
}