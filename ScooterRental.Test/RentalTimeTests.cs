using System;
using Xunit;

namespace ScooterRental.Test
{
    public class RentalTimeTests
    {
        private RentalTime _sut;
        public RentalTimeTests()
        {
            _sut = new RentalTime("testId", 0.2m, new DateTime(2021, 8, 8, 0, 0, 0));
        }

        [Fact]
        public void End_DateTime_DateTimeAssignedToProperty()
        {
            // Arrange
            var endTime = new DateTime(2021, 8, 8, 1, 0, 0);

            // Act
            _sut.End(endTime);
            var actual = _sut.EndTime;

            // Assert
            Assert.Equal(endTime, actual);
        }

        [Fact]
        public void End_RentalDuration(date)

    }
}