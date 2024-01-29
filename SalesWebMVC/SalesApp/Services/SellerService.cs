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
    }
}
