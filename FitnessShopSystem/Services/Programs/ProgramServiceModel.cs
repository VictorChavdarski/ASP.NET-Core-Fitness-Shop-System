namespace FitnessShopSystem.Services.Programs
{
    public class ProgramServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }
    }
}
