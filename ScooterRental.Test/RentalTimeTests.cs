using System;
using ScooterRental.Exceptions;
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
        public void End_GoodDateTime_DateTimeAssignedToProperty()
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
        public void End_DateTimeOlderThanStartTime_OlderThanStartTimeException()
        {
            // Arrange
            var endTime = new DateTime(2020, 7, 8, 1, 0, 0);
            var expectedMessage = "EndTime cannot be before StartTime";

            // Act
            Action act = () => _sut.End(endTime);

            // Assert
            EndTimeBeforeStartTimeException exception = Assert.Throws<EndTimeBeforeStartTimeException>(act);
        }

        [Fact]
        public void RentalDuration_CurrentTimeBeforeStartTime_EndTimeBeforeStartTimeException()
        {
            // Arrange
            var endTime = new DateTime(2020, 7, 8, 1, 0, 0);
            var expectedMessage = "EndTime cannot be before StartTime";

            // Act
            Action act = () => _sut.RentalDuration(endTime);

            // Assert
            EndTimeBeforeStartTimeException exception = Assert.Throws<EndTimeBeforeStartTimeException>(act);
        }

        [Fact]
        public void RentalDuration_CurrentTimeValid_ReturnsTimeSpan()
        {
            // Arrange
            var endTime = new DateTime(2021, 8, 9, 1, 0, 0);
            var expected = endTime - _sut.StartTime;

            // Act
            var actual = _sut.RentalDuration(endTime);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RentalDuration_ValidEndTimeSet_ReturnsTimeSpan()
        {
            // Arrange
            var endTime = new DateTime(2021, 8, 9, 1, 0, 0);
            var expected = endTime - _sut.StartTime;

            // Act
            _sut.End(endTime);
            var actual = _sut.RentalDuration();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}