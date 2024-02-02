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
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sl, DepartmentService departmentDervice)
        {
            _sellerService = sl;
            _departmentService = departmentDervice;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var deparments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = deparments };
            return View(viewModel);
        }

        [HttpPost] //Indicando que é uma ação de insert, ou seja, post
        [ValidateAntiForgeryToken] /*
                                    * A anotação [ValidateAntiForgeryToken] é comumente usada em desenvolvimento web, especialmente em frameworks como ASP.NET, para proteger contra ataques de falsificação de solicitação entre sites (CSRF - Cross-Site Request Forgery).
                            O CSRF ocorre quando um atacante faz com que um usuário autenticado envie uma solicitação não autorizada em um site, na qual o usuário já está autenticado. Ao adicionar a anotação [ValidateAntiForgeryToken] a um método ou ação em um controlador, você está garantindo que a solicitação seja acompanhada por um token específico, que deve corresponder ao token associado ao usuário autenticado. Isso dificulta a execução bem-sucedida de ataques CSRF, já que o token deve ser fornecido junto com a solicitação.

                            Em resumo, o uso de [ValidateAntiForgeryToken] é uma prática de segurança importante para proteger contra ataques CSRF em aplicativos da web.*/
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid) // Condição que verifica se o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityExceptions e)
            {
                return RedirectToAction(nameof(Error), new { message = "Can't delete seller, because he/she das sales" });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid) // Condição que valida se o modelo foi validado
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
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
