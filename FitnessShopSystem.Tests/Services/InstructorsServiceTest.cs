namespace FitnessShopSystem.Tests.Services
{
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Tests.Mocks;
    using FitnessShopSystem.Services.Instructors;

    using Xunit;

    public class InstructorsServiceTest
    {
        const string UserId = "TestUserId";

        [Fact]
        public void IsInstructorShouldReturnTrueWhenUserIsInstructor()
        {

            using var data = DatabaseMock.Instance;

            data.Instructors.Add(new Instructor { UserId = UserId });
            data.SaveChanges();

            var instructorService = new InstructorService(data);

            var result = instructorService.IsInstructor(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsInstructorShouldReturnFalseWhenUserIsNotInstructor()
        {
            using var data = DatabaseMock.Instance;

            data.Instructors.Add(new Instructor { UserId = UserId });
            data.SaveChanges();

            var instructorService = new InstructorService(data);

            var result = instructorService.IsInstructor("AnotherUserId");

            Assert.False(result);
        }
    }
}
