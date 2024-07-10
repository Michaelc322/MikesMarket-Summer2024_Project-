using CRM.Library.Models;
using CRM.Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRM.MAUI.ViewModels
{
    public class CartSelectionViewModel : INotifyPropertyChanged
    {
        public ShoppingCart InitialCart { get; set; }
        public CartSelectionViewModel() {
            InitialCart = ShoppingCartService.Current?.AddOrUpdate(new ShoppingCart { Name = "Shopping Cart" });
        }

        public ShoppingCart SelectedCart { get; set; }

        public List<ShoppingCart> ShoppingCarts { 
            get { 
                return ShoppingCartService.Current.Carts.ToList();

            } 
        }

        public void SwitchCart()
        {
            if (SelectedCart == null)
            {
                return;
            }

            Shell.Current.GoToAsync($"//Shop?cartId={SelectedCart.Id}");

            NotifyPropertyChanged(nameof(ShoppingCarts));

        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(ShoppingCarts));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
