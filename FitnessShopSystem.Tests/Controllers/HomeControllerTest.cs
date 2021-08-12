namespace FitnessShopSystem.Tests.Controllers
{
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Tests.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public vodi 

        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null, MapperMock.Instance);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
