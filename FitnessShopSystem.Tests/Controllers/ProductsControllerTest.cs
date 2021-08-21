namespace FitnessShopSystem.Tests.Controllers
{
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class ProductsControllerTest
    {
        [Fact]
        public void AddShouldReturnView()
        {
            var productController = new ProductsController(null, null, null);

            var result = productController.Add();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
