namespace FitnessShopSystem.Services.Manufacturers
{
    using System.Linq;
    using AutoMapper;
    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;
    using FitnessShopSystem.Services.Manufacturers.Models;

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

        public int Create(string name, string phoneNumber,string company, string userId)
        {
            var manufacturerData = new Manufacturer
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Company = company,
                UserId = userId
            };

            this.data.Manufacturers.Add(manufacturerData);
            this.data.SaveChanges();

            return manufacturerData.Id;
        }
    }
}
