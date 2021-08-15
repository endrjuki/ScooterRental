using System;

namespace ScooterRental
{
    public class RentalTime
    {
        private string _id;
        private decimal _pricePerMinute;
        private decimal _totalCost;
        private DateTime _startTime;
        private DateTime _endTime;

        public decimal Cost => _totalCost;
        public string Id => _id;
        public decimal PricePerMinute => _pricePerMinute;
        public DateTime StartTime => _startTime;
        public DateTime EndTime => _endTime;
        
        public RentalTime(decimal pricePerMinute, DateTime startTime)
        {
            _pricePerMinute = pricePerMinute;
            _startTime = startTime;
        }

        public void End(DateTime endTime)
        {
            _endTime = endTime;
            _totalCost = (endTime - _startTime).Minutes * _pricePerMinute;
        }

        public decimal CurrentCost(DateTime currentTime)
        {
            return _totalCost = (currentTime - _startTime).Minutes * _pricePerMinute;
        }
    }
}