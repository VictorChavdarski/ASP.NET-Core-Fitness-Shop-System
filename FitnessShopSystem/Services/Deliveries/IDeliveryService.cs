namespace FitnessShopSystem.Services.Deliveries
{
    using System.Threading.Tasks;
    public interface IDeliveryService
    {
        Task CreateAsync(string customerFirstName,
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
