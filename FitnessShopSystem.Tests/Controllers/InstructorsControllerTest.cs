namespace FitnessShopSystem.Tests.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Controllers;
    using FitnessShopSystem.Services.Instructors;

    using Xunit;

    public class InstructorsControllerTest
    {
        [Fact]
        public void CreateShouldReturnView()
        {
            var instructorsController = new InstructorsController(null);

            var result = instructorsController.Create();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnTask()
        {
            var instructorController = new InstructorsController(null);

            var result = instructorController.Create(null);

            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;

            var instructors = Enumerable.Range(0, 5).Select(i => new Instructor());

            data.Instructors.AddRange(instructors);
            data.SaveChanges();

            var instructorService = new InstructorService(data);
            var instructorController = new InstructorsController(instructorService);

            var result = instructorController.Create();

            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
