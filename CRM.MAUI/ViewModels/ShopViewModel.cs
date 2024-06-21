

using CRM.Library.Models;
using CRM.Library.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRM.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
        }

        private string inventoryQuery;
        public string InventoryQuery
        {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return inventoryQuery; }
        }


        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
            set { }
        }


        //private Product productToBuy;
        public ProductViewModel ProductToBuy { get; set; }
        public List<ProductViewModel> CartContents
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null).Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public string CartTotal
        {
            get
            {
                decimal totalPrice = CartContents
                                        .Sum(p => p.Product.Price * p.Product.Quantity);
                return $"Total: {totalPrice:C}";
            }
        }

        public ShoppingCart Cart
        {
            get
            {  
               return ShoppingCartService.Current?.Carts?.FirstOrDefault() ?? new ShoppingCart(); 
            }
        }

        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(ProductToBuy));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void PlaceInCart()
        {

            if (ProductToBuy?.Product != null)
            {

                ShoppingCartService.Current.AddToCart(ProductToBuy?.Product);
                //Cart?.Contents?.Add(ProductToBuy.Product);
             
            }
            NotifyPropertyChanged(nameof(Cart));
            NotifyPropertyChanged(nameof(CartContents));
            NotifyPropertyChanged(nameof(CartTotal));
            Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
