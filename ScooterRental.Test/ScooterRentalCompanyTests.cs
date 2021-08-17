using System;
using System.Collections.Generic;
using AutoFixture;
using ScooterRental.Exceptions;
using Xunit;
using NSubstitute;

namespace ScooterRental.Test
{

    public class ScooterRentalCompanyTests
    {
        private IRentalCompany _sut;
        private IScooterService _scooterService = Substitute.For<IScooterService>();
        private IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private IRentalService _rentalService = Substitute.For<IRentalService>();
        private IFixture _fixture = new Fixture();

        ScooterRentalCompanyTests()
        {
            _sut = new ScooterRentalCompany("testCompany", _dateTimeProvider, _scooterService, _rentalService);
        }


        [Theory]
        [InlineData()]
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

    class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime DateTimeNow { get; }
    }
}