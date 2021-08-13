namespace FitnessShopSystem.Services.Instructors
{
    public interface IInstructorService
    {
        public bool IsInstructor(string userId);

        public int GetId(string userId);
    }
}
