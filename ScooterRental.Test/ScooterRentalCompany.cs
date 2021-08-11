using System;
using Xunit;

namespace ScooterRental.Test
{
    public class ScooterRental
    {
        private ScooterRentalCompany _testCompany;
        
        public ScooterRental()
        {
            _testCompany = new ScooterRentalCompany("TestCompany");
        }
        
        [Fact]
        public void AddScooter_IdPricePerMinute_Successful()
        {
            string id = "testScooter";
            decimal pricePerMinute = 0.22m;
            
            _testCompany.AddScooter(id, pricePerMinute);
            var actual = _testCompany.GetScooterById(id);
            
            Assert.Equal(id, actual.Id);
            Assert.Equal(pricePerMinute, actual.PricePerMinute);
        }
    }
}