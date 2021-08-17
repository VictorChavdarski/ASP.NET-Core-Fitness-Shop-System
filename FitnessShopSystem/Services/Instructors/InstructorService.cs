namespace FitnessShopSystem.Services.Instructors
{
    using System.Linq;

    using FitnessShopSystem.Data;

    public class InstructorService : IInstructorService
    {
        private readonly FitnessShopDbContext data;

        public InstructorService(FitnessShopDbContext data) 
            => this.data = data;

        public bool IsInstructor(string userId)
               => this.data
                .Instructors
                .Any(i => i.UserId == userId);

        public int GetId(string userId)
         => this.data
                .Instructors
                .Where(i => i.UserId == userId)
                .Select(i => i.Id)
                .FirstOrDefault();
    }
}
