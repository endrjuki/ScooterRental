using System;
using ScooterRental.Exceptions;

namespace ScooterRental
{
    public class RentalTime
    {
        private string _id;
        private decimal _pricePerMinute;
        private DateTime _startTime;
        private DateTime _endTime;

        public virtual string Id => _id;
        public virtual decimal PricePerMinute => _pricePerMinute;
        public virtual DateTime StartTime => _startTime;
        public virtual DateTime EndTime => _endTime;

        public RentalTime(string scooterId, decimal pricePerMinute, DateTime startTime)
        {
            _id = scooterId;
            _pricePerMinute = pricePerMinute;
            _startTime = startTime;
        }

        public void End(DateTime endTime)
        {
            if (endTime < StartTime)
            {
                throw new EndTimeBeforeStartTimeException("EndTime cannot be before StartTime");
            }

            _endTime = endTime;
        }

        public TimeSpan RentalDuration(DateTime currentTime)
        {
            if (currentTime < StartTime)
            {
                throw new EndTimeBeforeStartTimeException("EndTime cannot be before StartTime");
            }

            return currentTime - StartTime;
        }

        public TimeSpan RentalDuration()
        {
            return EndTime - StartTime;
        }
    }
}