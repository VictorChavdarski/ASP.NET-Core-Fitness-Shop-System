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
                .Any(m => m.UserId == userId);

        public int GetId(string userId)
         => this.data
                .Instructors
                .Where(m => m.UserId == userId)
                .Select(m => m.Id)
                .FirstOrDefault();
    }
}
