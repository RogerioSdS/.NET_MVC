using Microsoft.AspNetCore.Mvc;
using SalesApp.Services;

namespace SalesApp.Controllers
{
    public class SellersController : Controller
    {
        //Criando a dependencia da injeção, para injetar a dependência da classe SellerService no construtor da classe SellersController.
        private readonly SellerService _sellerService;
        public SellersController(SellerService sl)
        {
            _sellerService = sl;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

    }
}
