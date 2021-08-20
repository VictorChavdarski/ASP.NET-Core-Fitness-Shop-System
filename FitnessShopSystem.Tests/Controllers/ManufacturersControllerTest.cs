namespace FitnessShopSystem.Tests.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Services.Manufacturers;

    using Xunit;

    public class ManufacturersControllerTest
    {
        [Fact]
        public void CreateShouldReturnView()
        {
            var manufacturersController = new ManufacturesController(null);

            var result = manufacturersController.Create();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnTask()
        {
            var manufacturerController = new ManufacturesController(null);

            var result = manufacturerController.Create(null);

            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void CreateShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;

            var manufacturers = Enumerable.Range(0, 5).Select(i => new Manufacturer());

            data.Manufacturers.AddRange(manufacturers);
            data.SaveChanges();

            var manufacturerService = new ManufacturerService(data);
            var manufacturerController = new ManufacturesController(manufacturerService);

            var result = manufacturerController.Create();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
