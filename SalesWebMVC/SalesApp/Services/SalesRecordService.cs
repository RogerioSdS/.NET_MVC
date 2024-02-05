using Microsoft.EntityFrameworkCore;
using SalesApp.Data;
using SalesApp.Models;

namespace SalesApp.Services
{
    public class SalesRecordService
    {
        private readonly SalesAppContext _Context;

        public SalesRecordService(SalesAppContext context)
        {
            _Context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _Context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x =>x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        //No metodo abaixo, iremos agrupar o retorno da nossa consulta, por isso estamos passando uma lista do tipo IGrouping, passando o Department e o SalesRecord como parametros
        public async Task<List<IGrouping<Department , SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _Context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }

            var data = await result

                .Include(x => x.Seller)

                .Include(x => x.Seller.Department)

                .OrderByDescending(x => x.Date)

                .ToListAsync();

            return data.GroupBy(x => x.Seller.Department).ToList();
           
        }
    }
}
