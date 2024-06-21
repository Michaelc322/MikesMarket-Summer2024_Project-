using CRM.Library.Models;

namespace CRM.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }

        public List<Product>? Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<Product>();
        }
    }
}
