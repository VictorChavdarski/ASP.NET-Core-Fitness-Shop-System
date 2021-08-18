namespace FitnessShopSystem.Services.Instructors
{
    public interface IInstructorService
    {
        public bool IsInstructor(string userId);

        public int GetId(string userId);

        int Create(
        string firstName,
        string lastName,
        int age,
        string phoneNumber,
        string email,
        string userId
        );

    }
}
