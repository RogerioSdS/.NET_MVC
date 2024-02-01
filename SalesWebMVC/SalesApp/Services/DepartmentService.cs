using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Department>> FindAllAsync()
        {
            return await _Context.Department.OrderBy(x => x.Name).ToListAsync(); // Lembrando que a operação Linq é apenas a query, necessita ser executada, que nesse caso a execução é o Tolist()
            //O ToList() é uma operação sincrona, por isso vamos alterar para o metodo ToListAsync()
            //Inserimos a palavra await para informar ao compilador que a chamada será assincrona
        }
    }
}
