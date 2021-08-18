namespace FitnessShopSystem.Services.Deliveries
{
    public interface IDeliveryService
    {
        int Create(string customerFirstName,
            string customerLastName,
            string company,
            string address,
            int postalCode,
            string city,
            string email,
            string country,
            string phone,
            string userId,
            int productId);
    }
}
