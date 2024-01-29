using Microsoft.AspNetCore.Mvc;

namespace SalesApp.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
