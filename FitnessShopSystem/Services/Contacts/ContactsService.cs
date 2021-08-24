namespace FitnessShopSystem.Services.Contacts
{
    using System.Threading.Tasks;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Data.Models;

    public class ContactsService : IContactsService
    {
        private readonly FitnessShopDbContext data;

        public ContactsService(FitnessShopDbContext data)
            => this.data = data;

        public async Task CreateAsync(string firstName, string lastName, string email, string country, string subject, string userId)
        {
            var contactData = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Country = country,
                Subject = subject,
                UserId = userId
            };

            await this.data.AddAsync(contactData);
            await this.data.SaveChangesAsync();
        }
    }
}
