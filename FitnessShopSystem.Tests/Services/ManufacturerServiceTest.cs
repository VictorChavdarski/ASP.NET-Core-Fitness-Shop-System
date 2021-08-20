namespace FitnessShopSystem.Tests.Services
{
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Services.Manufacturers;

    using Xunit;

    public class ManufacturerServiceTest
    {
        private const string UserId = "TestUserId";

        [Fact]
        public void IsManufacturerShouldReturnTrueWhenUserIsDealer()
        {
            var manufacturerService = GetManufacturerService();

            var result = manufacturerService.IsManufacturer(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsManufacturerShouldReturnFalseWhenUserIsDealer()
        {
            var manufacturerService = GetManufacturerService();

            var result = manufacturerService.IsManufacturer("AnotherUserId");

            Assert.False(result);
        }

        private static IManufacturerService GetManufacturerService()
        {
            var data = DatabaseMock.Instance;

            data.Manufacturers.Add(new Manufacturer { UserId = UserId });
            data.SaveChanges();

            return new ManufacturerService(data);
        }
    }
}
