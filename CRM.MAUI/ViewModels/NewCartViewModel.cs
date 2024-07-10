using CRM.Library.Models;
using CRM.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.MAUI.ViewModels
{
    public class NewCartViewModel
    {

        public ShoppingCart? Cart { get; set; }

        public string? Name
        {
            get
            {
                return Cart?.Name ?? string.Empty;
            }

            set
            {
                if (Cart != null)
                {
                    Cart.Name = value;
                }
            }
        }

        public NewCartViewModel()
        {
            Cart = new ShoppingCart();
            Cart.Id = 0;
        }

        public NewCartViewModel(int Id)
        {
            Cart = new ShoppingCart();
            Cart.Id = Id;
        }

        public void Add()
        {
            if (Cart != null)
            {
                //Cart.Id = ShoppingCartService.Current.Carts?.Select(i => i.Id)?.Max() + 1 ?? 0;
                ShoppingCartService.Current.AddOrUpdate(Cart);
            }
        }
    }
}
