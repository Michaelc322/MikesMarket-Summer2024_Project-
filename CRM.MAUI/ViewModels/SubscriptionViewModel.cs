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
    public class SubscriptionViewModel : INotifyPropertyChanged
    {
        public ProductViewModel? SelectedProduct { get; set; }

        private ProductViewModel? productToBuy;

        public ProductViewModel? ProductToBuy
        {
            get => productToBuy;

            set
            {
                productToBuy = value;

                if (productToBuy != null && productToBuy.Product == null)
                {
                    productToBuy.Product = new Product();
                }
                else if (productToBuy != null && productToBuy.Product != null)
                {
                    productToBuy.Product = new Product(productToBuy.Product);
                }
            }
        }

        public string Frequency { get; set; }
        public int subscribedQuantity { get; set; }

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

        public ShoppingCart SubscriptionCart { get; set; }

        public SubscriptionViewModel()
        {
            InventoryQuery = string.Empty;
            Product = new Product();
            SubscriptionCart = ShoppingCartService.Current?.AddOrUpdate(new ShoppingCart { Name = "Subscriptions" });
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

        public List<ProductViewModel> ProductsInSubscription
        {
            get
            {
                return ShoppingCartService.Current?.Carts?.FirstOrDefault(c => c.Id == SubscriptionCart?.Id)?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public SubscriptionViewModel(int productId)
        {

            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == productId);
            if (Product == null)
            {
                Product = new Product();
            }
        }
        public Product? Product { get; set; }

        public void AddSubscription()
        {
            if (ProductToBuy?.Product == null)
            {
                return;
            }

            Shell.Current.GoToAsync($"//ProductSub?productId={ProductToBuy.Product.Id}&cartId={SubscriptionCart.Id}");

            ProductToBuy = null;

            NotifyPropertyChanged(nameof(ProductsInSubscription));

            NotifyPropertyChanged(nameof(Products));

        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(ProductsInSubscription));
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
