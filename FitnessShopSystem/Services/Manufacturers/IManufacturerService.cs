using FitnessShopSystem.Services.Manufacturers.Models;

namespace FitnessShopSystem.Services.Manufacturers
{
    public interface IManufacturerService
    {
        public bool IsManufacturer(string userId);

        public int GetId(string userId);

        int Create(
          string name,
          string phoneNumber,
          string company,
          string userId
          );
         
    }
}
