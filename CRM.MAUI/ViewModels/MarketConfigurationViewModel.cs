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
    class MarketConfigurationViewModel : INotifyPropertyChanged
    {
        public ProductViewModel? SelectedProduct { get; set; }

        public decimal taxRate { get; set; }

        public MarketConfigurationViewModel()
        {
            Product = new Product();
        }

        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.Where(p => p != null).Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public MarketConfigurationViewModel(int productId)
        {

            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == productId);
            if (Product == null)
            {
                Product = new Product();
            }
        }
        public Product? Product { get; set; }


        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateContact()
        {
            if (SelectedProduct?.Product == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Configuration?productId={SelectedProduct.Product.Id}");
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }


        public void UpdateTaxRate()
        {
            InventoryServiceProxy.Current.ChangeTax(taxRate);
        }
    }
}
