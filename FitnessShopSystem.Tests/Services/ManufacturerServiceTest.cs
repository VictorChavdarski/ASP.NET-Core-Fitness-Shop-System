namespace FitnessShopSystem.Tests.Services
{
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Services.Manufacturers;
    using FitnessShopSystem.Tests.Mocks;
    using Xunit;

    public class ManufacturerServiceTest
    {
        [Fact]
        public void IsManufacturerShouldReturnTrueWhenUserIsDealer()
        {
            //Arrange
            const string userId = "TestUserId";

            using var data = DatabaseMock.Instance;

            data.Manufacturers.Add(new Manufacturer { UserId = userId});
            data.SaveChanges();

            var manufacturerService = new ManufacturerService(data);

            //Act
            var result = manufacturerService.IsManufacturer(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsManufacturerShouldReturnFalseWhenUserIsDealer()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            data.Manufacturers.Add(new Manufacturer { UserId = "TestUserId" });
            data.SaveChanges();

            var manufacturerService = new ManufacturerService(data);

            //Act
            var result = manufacturerService.IsManufacturer("AnotherUserId");

            //Assert
            Assert.False(result);
        }

    }
}
