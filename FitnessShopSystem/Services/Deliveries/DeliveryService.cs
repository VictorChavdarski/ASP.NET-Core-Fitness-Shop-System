using FitnessShopSystem.Data;
using FitnessShopSystem.Data.Models;

namespace FitnessShopSystem.Services.Deliveries
{
    public class DeliveryService : IDeliveryService
    {
        private readonly FitnessShopDbContext data;

        public DeliveryService(FitnessShopDbContext data)
            => this.data = data;

        public int Create(string customerFirstName, string customerLastName, string company, string address, int postalCode, string city, string email, string country, string phone, string userId, int productId)
        {
            var deliveryData = new Delivery
            {
                CustomerFirstName = customerFirstName,
                CustomerLastName = customerLastName,
                Company = company,
                Address = address,
                PostalCode = postalCode,
                City = city,
                Email = email,
                Country = country,
                Phone = phone,
                UserId = userId,
                ProductId = productId
            };

            this.data.Deliveries.Add(deliveryData);
            this.data.SaveChanges();

            return deliveryData.Id;
        }
    }
}
