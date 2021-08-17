using System;
using System.Collections.Generic;
using AutoFixture;
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
        public void StartRent_ValidId_StartsRental()
        {
            // Arrange
            var testScooter = _fixture.Build<Scooter>().Create();
            _scooterService.GetScooterById(testScooter.Id).Returns(testScooter);
            _dateTimeProvider.DateTimeNow.Returns((new DateTime(2021, 1, 1, 0, 0, 0)));

            // Action
            _sut.StartRent(testScooter.Id);

            // Assert
            _scooterService.Received(1).GetScooterById(Arg.Is<string>(s => s == testScooter.Id));
            _rentalService.Received(1).RentScooter(Arg.Is<Scooter>(s => s.Id == testScooter.Id &&
                                                                        s.PricePerMinute == testScooter.PricePerMinute),
                Arg.Is<DateTime>(time => time == _dateTimeProvider.DateTimeNow));
        }

        [Fact]
        public void EndRent()
        {

        }

        [Fact]
        public void CalculateIncome()
        {

        }

    }
}