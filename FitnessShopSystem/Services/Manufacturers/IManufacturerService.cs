namespace FitnessShopSystem.Services.Manufacturers
{
    using System.Threading.Tasks;

    public interface IManufacturerService
    {
        public bool IsManufacturer(string userId);

        public int GetId(string userId);

        Task CreateAsync(
          string name,
          string phoneNumber,
          string company,
          string userId
          );

    }
}
