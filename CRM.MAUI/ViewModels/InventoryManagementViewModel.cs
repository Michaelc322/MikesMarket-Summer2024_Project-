using CRM.Library.Models;
using CRM.Library.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CRM.MAUI.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public ProductViewModel? SelectedProduct { get; set; }
        public InventoryManagementViewModel()
        {

        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current?.Products?.Where(p=>p != null).Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

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
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct.Product.Id}");
            InventoryServiceProxy.Current.AddOrUpdate(SelectedProduct.Product);
        }

        public void DeleteProduct()
        {
            if (SelectedProduct?.Product == null)
            {
                return;
            }

            InventoryServiceProxy.Current.Delete(SelectedProduct.Product.Id);
            Refresh();
        }
    }
}
