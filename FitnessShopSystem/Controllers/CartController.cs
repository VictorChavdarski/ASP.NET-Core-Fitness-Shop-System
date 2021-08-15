namespace FitnessShopSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CartController : Controller
    {
        public IActionResult Order()
        {
            return View();
        }
    }
}
