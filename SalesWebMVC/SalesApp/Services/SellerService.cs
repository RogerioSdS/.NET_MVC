using SalesApp.Data;
using SalesApp.Models;

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
            return _Context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _Context.Seller.Find(id);
            _Context.Seller.Remove(obj);
            _Context.SaveChanges();
        }

    }
}
