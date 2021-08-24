namespace FitnessShopSystem.Services.Contacts
{
    using System.Threading.Tasks;

    public interface IContactsService
    {
        Task CreateAsync(
            string firstName,
            string lastName,
            string email,
            string country,
            string subject,
            string userId
            );
    }
}
