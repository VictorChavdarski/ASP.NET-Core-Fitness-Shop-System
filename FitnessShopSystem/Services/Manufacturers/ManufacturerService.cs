namespace FitnessShopSystem.Services.Manufacturers
{
    using System.Linq;
    using FitnessShopSystem.Data;

    public class ManufacturerService : IManufacturerService
    {
        private readonly FitnessShopDbContext data;

        public ManufacturerService(FitnessShopDbContext data)
            => this.data = data;

        public bool IsManufacturer(string userId)
            => this.data
                .Manufacturers
                .Any(m => m.UserId == userId);

        public int GetId(string userId)
            => this.data
                .Manufacturers
                .Where(m => m.UserId == userId)
                .Select(m => m.Id)
                .FirstOrDefault();
    }
}
