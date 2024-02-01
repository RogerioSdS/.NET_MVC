using SalesApp.Data;
using SalesApp.Models;
using Microsoft.EntityFrameworkCore;
using SalesApp.Services.Exceptions;
using System.Data;

namespace SalesApp.Services
{
    public class SellerService
    {
        private readonly SalesAppContext _Context;

        public SellerService(SalesAppContext context)
        {
            _Context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _Context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _Context.Add(seller); // A operação add é feito somente em memória
            await _Context.SaveChangesAsync(); //A operação SaveChanges que irá acessar o db, por isso somente ela quem será assincrona
        }


        public async Task<Seller> FindByIdAsync(int id) 
        {
            //Fazendo o eafer loading, que é carregar outros objetos ligados ao objeto principal, usando o Include
            Seller? seller = await _Context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
            return seller;
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _Context.Seller.FindAsync(id);
            _Context.Seller.Remove(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            var hasAny = await _Context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _Context.Update(obj);
                await _Context.SaveChangesAsync();
            }
            catch (DBConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
