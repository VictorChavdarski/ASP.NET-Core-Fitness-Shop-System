namespace FitnessShopSystem.Services.Manufacturers
{
    public interface IManufacturerService
    {
        public bool IsManufacturer(string userId);

        public int GetId(string userId);
    }
}
