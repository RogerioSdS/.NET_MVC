using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Services
{
    public class DepartmentService
    {
        private readonly SalesAppContext _Context;

        public DepartmentService(SalesAppContext context)
        {
            _Context = context;
        }

        public List<Department> FindAll()
        {
            return _Context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
