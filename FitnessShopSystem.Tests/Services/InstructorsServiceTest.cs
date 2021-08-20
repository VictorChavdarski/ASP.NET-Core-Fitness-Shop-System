namespace FitnessShopSystem.Tests.Services
{
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Services.Instructors;

    using Xunit;

    public class InstructorsServiceTest
    {
        [Fact]
        public void IsInstructorShouldReturnTrueWhenUserIsInstructor()
        {
            const string userId = "TestUserId";

            using var data = DatabaseMock.Instance;

            data.Instructors.Add(new Instructor { UserId = userId });
            data.SaveChanges();

            var instructorService = new InstructorService(data);

            var result = instructorService.IsInstructor(userId);

            Assert.True(result);
        }

        [Fact]
        public void IsInstructorShouldReturnFalseWhenUserIsNotInstructor()
        {
            using var data = DatabaseMock.Instance;

            data.Instructors.Add(new Instructor { UserId = "TestUserId" });
            data.SaveChanges();

            var instructorService = new InstructorService(data);

            var result = instructorService.IsInstructor("AnotherUserId");

            Assert.False(result);
        }
    }
}
