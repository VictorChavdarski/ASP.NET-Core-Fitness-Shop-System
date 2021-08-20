namespace FitnessShopSystem.Services.Instructors
{
    using System.Threading.Tasks;

    public interface IInstructorService
    {
        public bool IsInstructor(string userId);

        public int GetId(string userId);

        Task CreateAsync(
        string firstName,
        string lastName,
        int age,
        string phoneNumber,
        string email,
        string userId
        );

    }
}
