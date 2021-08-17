using System;

namespace ScooterRental
{
    public class RentalTime
    {
        private string _id;
        private decimal _pricePerMinute;
        private DateTime _startTime;
        private DateTime _endTime;

        public string Id => _id;
        public decimal PricePerMinute => _pricePerMinute;
        public DateTime StartTime => _startTime;
        public DateTime EndTime => _endTime;

        public RentalTime(string scooterId, decimal pricePerMinute, DateTime startTime)
        {
            _id = scooterId;
            _pricePerMinute = pricePerMinute;
            _startTime = startTime;
        }

        public void End(DateTime endTime)
        {
            _endTime = endTime;
        }

        public TimeSpan RentalDuration(DateTime currentTime)
        {
            return currentTime - _startTime;
        }
    }
}