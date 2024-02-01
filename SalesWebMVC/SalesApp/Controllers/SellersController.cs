using Microsoft.AspNetCore.Mvc;
using SalesApp.Models;
using SalesApp.Models.ViewModels;
using SalesApp.Services;
using SalesApp.Services.Exceptions;
using System.Diagnostics;




namespace SalesApp.Controllers
{
    public class SellersController : Controller
    {
        //Criando a dependencia da injeção, para injetar a dependência da classe SellerService no construtor da classe SellersController.
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentDervice;
        public SellersController(SellerService sl, DepartmentService departmentDervice)
        {
            _sellerService = sl;
            _departmentDervice = departmentDervice;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var deparments = _departmentDervice.FindAll();
            var viewModel = new SellerFormViewModel { Departments = deparments };
            return View(viewModel);
        }

        [HttpPost] //Indicando que é uma ação de insert, ou seja, post
        [ValidateAntiForgeryToken] /*
                                    * A anotação [ValidateAntiForgeryToken] é comumente usada em desenvolvimento web, especialmente em frameworks como ASP.NET, para proteger contra ataques de falsificação de solicitação entre sites (CSRF - Cross-Site Request Forgery).
                            O CSRF ocorre quando um atacante faz com que um usuário autenticado envie uma solicitação não autorizada em um site, na qual o usuário já está autenticado. Ao adicionar a anotação [ValidateAntiForgeryToken] a um método ou ação em um controlador, você está garantindo que a solicitação seja acompanhada por um token específico, que deve corresponder ao token associado ao usuário autenticado. Isso dificulta a execução bem-sucedida de ataques CSRF, já que o token deve ser fornecido junto com a solicitação.

                            Em resumo, o uso de [ValidateAntiForgeryToken] é uma prática de segurança importante para proteger contra ataques CSRF em aplicativos da web.*/
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided"});
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = _departmentDervice.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }            
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
