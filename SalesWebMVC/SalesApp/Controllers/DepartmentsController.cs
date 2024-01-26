using Microsoft.AspNetCore.Mvc;
using SalesApp.Models;

namespace SalesApp.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {
            List<Department> listDepart = new List<Department>();
            listDepart.Add(new Department { Id = 1, Name = "Eltronics"});
            listDepart.Add(new Department { Id = 2, Name = "Fashion"});

            return View(listDepart);
        }
    }
}
