namespace FitnessShopSystem.Tests.Controllers
{
    using System.Linq;

    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Models.Home;
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Services.Products;

    using Microsoft.AspNetCore.Mvc;

    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController( null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var products = Enumerable.Range(0, 10).Select(i => new Product());

            data.Products.AddRange(products);
            data.SaveChanges();

            var productService = new ProductService(data, mapper);
            var homeController = new HomeController(productService);

            var result = homeController.Index();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(10, indexViewModel.TotalProducts);
        }

        [Fact]
        public void ChatShouldReturnView()
        {
            var homeController = new HomeController(null);

            var result = homeController.Chat();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void JoinShouldReturnView()
        {
            var homeController = new HomeController(null);

            var result = homeController.Join();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
