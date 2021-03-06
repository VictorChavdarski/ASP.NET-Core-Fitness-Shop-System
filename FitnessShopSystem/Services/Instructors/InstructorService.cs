namespace FitnessShopSystem.Services.Instructors
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;

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

        public async Task CreateAsync(string firstName, string lastName, int age, string phoneNumber, string email, string userId)
        {
            var instruuctorData = new Instructor
            {
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                PhoneNumber = phoneNumber,
                Email = email,
                UserId = userId
            };

            await this.data.Instructors.AddAsync(instruuctorData);
            await this.data.SaveChangesAsync();
        }
    }
}
