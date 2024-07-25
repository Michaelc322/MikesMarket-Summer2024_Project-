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

        public async void Refresh()
        {
            await InventoryServiceProxy.Current.Get();
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Edit()
        {
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct?.Product?.Id ?? 0}");
        }

        public async void DeleteProduct()
        {
            if (SelectedProduct?.Product == null)
            {
                return;
            }

            await InventoryServiceProxy.Current.Delete(SelectedProduct.Product.Id);
            Refresh();
        }
    }
}
