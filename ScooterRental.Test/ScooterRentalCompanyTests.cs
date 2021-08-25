using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Moq;
using ScooterRental.Exceptions;
using Xunit;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ScooterRental.Test
{
    public class ScooterRentalCompanyTests
    {
        private IRentalCompany _sut;
        private IScooterService _scooterService = Substitute.For<IScooterService>();
        private IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private IRentalService _rentalService = Substitute.For<IRentalService>();
        private IFixture _fixture = new Fixture();

        public ScooterRentalCompanyTests()
        {
            _sut = new ScooterRentalCompany("testCompany", _dateTimeProvider, _scooterService, _rentalService);
        }

        [Fact]
        public void Name_ReturnsCompanyName()
        {
            // Arrange
            var expected = "testCompany";

            // Act
            var actual = _sut.Name;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StartRent_ValidId_InvokesMethodsWithProperArguments()
        {
            // Arrange
            var testScooter = _fixture.Build<Scooter>().Create();
            _scooterService.GetScooterById(testScooter.Id).Returns(testScooter);
            _dateTimeProvider.DateTimeNow.Returns((new DateTime(2021, 1, 1, 0, 0, 0)));

            // Act
            _sut.StartRent(testScooter.Id);

            // Assert
            _scooterService.Received(1).GetScooterById(Arg.Is<String>(s => s == testScooter.Id));
            _rentalService.Received(1).RentScooter(Arg.Is<Scooter>(s => s.Id == testScooter.Id &&
                                                                        s.PricePerMinute == testScooter.PricePerMinute),
                Arg.Is<DateTime>(time => time == _dateTimeProvider.DateTimeNow));
        }

        [Fact]
        public void EndRent_ValidId_ReturnsDecimal()
        {
            // Arrange
            var testScooter = _fixture.Build<Scooter>().Create();
            _scooterService.GetScooterById(testScooter.Id).Returns(testScooter);
            _dateTimeProvider.DateTimeNow.Returns((new DateTime(2021, 1, 1, 0, 0, 0)));
            _rentalService.ReturnScooter(testScooter, _dateTimeProvider.DateTimeNow).Returns(40);
            var expectedIncome = 40 * testScooter.PricePerMinute;

            // Act
            var actualIncome = _sut.EndRent(testScooter.Id);

            // Assert
            _scooterService.Received(1).GetScooterById(Arg.Is<String>(s => s == testScooter.Id));
            _rentalService.Received(1).ReturnScooter(Arg.Is<Scooter>(s => s.Id == testScooter.Id &&
                                                                          s.PricePerMinute == testScooter.PricePerMinute),
                Arg.Is<DateTime>(time => time == _dateTimeProvider.DateTimeNow));

            Assert.Equal(expectedIncome, actualIncome);
        }

        [Fact]
        public void CalculateIncome_ActiveRentalsAndCompleteRentals_ReturnsDecimal()
        {
            //Arrange
            var testYear = 2021;
            var id = "testId";
            var startTime = new DateTime(testYear, 8, 18, 0, 0, 0);
            var endTime = new DateTime(testYear, 8, 18, 2, 10, 0);
            var pricePerMinute = 0.20m;

            var mockedCompleteEntry = new Mock<RentalTime>(id, pricePerMinute, startTime);
            mockedCompleteEntry.SetupGet(rt => rt.StartTime).Returns(startTime);
            mockedCompleteEntry.SetupGet(rt => rt.EndTime).Returns(endTime);
            mockedCompleteEntry.SetupGet(rt => rt.PricePerMinute).Returns(pricePerMinute);

            var mockedInProgressEntry = new Mock<RentalTime>(id, pricePerMinute, startTime);
            mockedInProgressEntry.SetupGet(rt => rt.StartTime).Returns(startTime);
            mockedInProgressEntry.SetupGet(rt => rt.PricePerMinute).Returns(pricePerMinute);


            var activeRentalList = new List<RentalTime>()
            {
                mockedInProgressEntry.Object,
                mockedInProgressEntry.Object,
                mockedInProgressEntry.Object
            };

            var completeRentalList = new List<RentalTime>()
            {
                mockedCompleteEntry.Object,
                mockedCompleteEntry.Object
            };

            _rentalService.CurrentActiveRentals().Returns(activeRentalList);
            _rentalService.RentalHistory(testYear).Returns(completeRentalList);
            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 8, 18, 3 , 0, 0, 0));
            var expectedIncome = 160m;

            //Action
            var actualIncome = _sut.CalculateIncome(testYear, true);

            //Assert
            Assert.Equal(expectedIncome, actualIncome);
        }

        [Fact]
        public void CalculateIncome_OnlyCompleteRentals_ReturnsDecimal()
        {
            //Arrange
            var testYear = 2021;
            var id = "testId";
            var startTime = new DateTime(testYear, 8, 18, 0, 0, 0);
            var endTime = new DateTime(testYear, 8, 18, 2, 10, 0);
            var pricePerMinute = 0.20m;

            var mockedCompleteEntry = new Mock<RentalTime>(id, pricePerMinute, startTime);
            mockedCompleteEntry.SetupGet(rt => rt.StartTime).Returns(startTime);
            mockedCompleteEntry.SetupGet(rt => rt.EndTime).Returns(endTime);
            mockedCompleteEntry.SetupGet(rt => rt.PricePerMinute).Returns(pricePerMinute);

            var mockedInProgressEntry = new Mock<RentalTime>(id, pricePerMinute, startTime);
            mockedInProgressEntry.SetupGet(rt => rt.StartTime).Returns(startTime);
            mockedInProgressEntry.SetupGet(rt => rt.PricePerMinute).Returns(pricePerMinute);


            var activeRentalList = new List<RentalTime>()
            {
                mockedInProgressEntry.Object,
                mockedInProgressEntry.Object,
                mockedInProgressEntry.Object
            };

            var completeRentalList = new List<RentalTime>()
            {
                mockedCompleteEntry.Object,
                mockedCompleteEntry.Object
            };

            _rentalService.CurrentActiveRentals().Returns(activeRentalList);
            _rentalService.RentalHistory(testYear).Returns(completeRentalList);
            _dateTimeProvider.DateTimeNow.Returns(new DateTime(2021, 8, 18, 3 , 0, 0, 0));
            var expectedIncome = 52m;

            //Action
            var actualIncome = _sut.CalculateIncome(testYear, false);

            //Assert
            Assert.Equal(expectedIncome, actualIncome);
        }
    }
}