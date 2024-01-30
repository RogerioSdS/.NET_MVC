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
            seller.Department = _Context.Department.First();
            _Context.Add(seller);
            _Context.SaveChanges();
        }
    }
}
