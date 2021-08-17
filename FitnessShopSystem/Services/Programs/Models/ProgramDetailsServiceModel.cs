namespace FitnessShopSystem.Services.Programs.Models
{
    public class ProgramDetailsServiceModel : ProgramServiceModel
    {
        public int InstructorId { get; set; }

        public string InstructorName { get; set; }

        public string UserId { get; set; }
    }
}
