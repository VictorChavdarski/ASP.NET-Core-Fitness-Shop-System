namespace FitnessShopSystem.Services.Manufacturers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;

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

        public async Task CreateAsync(string name, string phoneNumber,string company, string userId)
        {
            var manufacturerData = new Manufacturer
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Company = company,
                UserId = userId
            };

            await this.data.Manufacturers.AddAsync(manufacturerData);
            await this.data.SaveChangesAsync();
        }
    }
}
