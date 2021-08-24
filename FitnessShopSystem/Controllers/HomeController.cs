namespace FitnessShopSystem.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using FitnessShopSystem.Data;
    using FitnessShopSystem.Models.Home;
    using FitnessShopSystem.Services.Products;
    using FitnessShopSystem.Services.Programs;
    using FitnessShopSystem.Services.Contacts;
    using FitnessShopSystem.Infrastructure;

    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IContactsService contacts;
        private readonly IProgramService programs;
        private readonly FitnessShopDbContext data;

        public HomeController(
            FitnessShopDbContext data,
            IProductService products,
            IProgramService programs,
            IContactsService contacts)
        {
            this.data = data;
            this.programs = programs;
            this.products = products;
            this.contacts = contacts;
        }

        public IActionResult Index()
        {
            var totalProducts = this.products.TotalProducts();

            var latestProducts = this.products.Latest();

            var latestPrograms = this.programs.Latest();

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = latestProducts,
                Programs = latestPrograms
            });
        }

        public IActionResult Error() => View();

        public IActionResult Join() => View();

        [Authorize]
        public IActionResult Chat() => View();

        [Authorize]
        public IActionResult Contact() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Contact(ContactFormModel contact) 
        {
            var userId = this.User.GetId();

            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            await this.contacts.CreateAsync(
                contact.FirstName,
                contact.LastName,
                contact.Email,
                contact.Country,
                contact.Subject,
                userId
                );

            return Redirect(nameof(Index));
        }
    }
}
