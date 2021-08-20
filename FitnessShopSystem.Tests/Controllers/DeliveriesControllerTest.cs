namespace FitnessShopSystem.Tests.Controllers
{
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Services.Deliveries;
    using FitnessShopSystem.Tests.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class DeliveriesControllerTest
    {
        [Fact]
        public void OrderShouldReturnView()
        {
            var deliveriesController = new DeliveriesController(null, null);

            var result = deliveriesController.Order();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void OrderShouldReturnTask()
        {
            var deliveriesController = new DeliveriesController(null, null);

            var result = deliveriesController.Order(0, null);

            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void OrderShouldReturnViewWithCorrectData()
        {
            var data = DatabaseMock.Instance;

            var deliveries = Enumerable.Range(0, 5).Select(i => new Delivery());

            data.Deliveries.AddRange(deliveries);
            data.SaveChanges();

            var deliveryService = new DeliveryService(data);
            var deliveryController = new DeliveriesController(null, deliveryService);

            var result = deliveryController.Order();

            Assert.NotNull(result);
            Assert.Equal(5, data.Deliveries.Count());
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
