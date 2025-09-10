using Shop_mvc_pv421.Extensions;
using Shop_mvc_pv421.Interfaces;

namespace Shop_mvc_pv421.Services
{
    public class CartService : ICartService
    {
        public int GetCartSize(HttpContext httpContext)
        {
            var ids = httpContext.Session.Get<List<int>>("CartItems");
            return ids?.Count ?? 0;
        }
    }
}
