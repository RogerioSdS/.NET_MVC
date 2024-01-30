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

        public List<Seller> FindAll()
        {
            return _Context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _Context.Add(seller);
            _Context.SaveChanges();
        }


        public Seller FindById(int id) 
        {
            //Fazendo o eafer loading, que é carregar outros objetos ligados ao objeto principal, usando o Include
            return _Context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _Context.Seller.Find(id);
            _Context.Seller.Remove(obj);
            _Context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if(!_Context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _Context.Update(obj);
                _Context.SaveChanges();
            }
            catch (DBConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
