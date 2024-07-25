

using CRM.Library.DTO;
using CRM.Library.Models;
using CRM.Library.Services;
using Microsoft.Maui.Graphics.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRM.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShoppingCart Cart { get; set; }
        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
            

        }

        public ShopViewModel(int CartID)
        {
            InventoryQuery = string.Empty;
            Cart = ShoppingCartService.Current?.Carts?.FirstOrDefault(c => c.Id == CartID);
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

        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return ShoppingCartService.Current?.Carts?.FirstOrDefault(c => c.Id == Cart?.Id)?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }


        private ProductViewModel? productToBuy;

        public ProductViewModel? ProductToBuy { 
            get => productToBuy;

            set
            {
                productToBuy = value;

                if (productToBuy != null && productToBuy.Product == null)
                {
                    productToBuy.Product = new ProductDTO();
                }
                else if(productToBuy != null && productToBuy.Product != null)
                {
                    productToBuy.Product = new ProductDTO(productToBuy.Product);
                }
            } 
        }

        public decimal Tax
        {
            get
            {
                return CartTotal * InventoryServiceProxy.Current.taxAmount;

            }
        }

        public string CartSubtotal
        {
            get
            {
                return $"Subtotal: {CartTotal:C}";
            }
        }

        public string CartTax
        {
            get
            {
                return $"Tax: {Tax:C}";
            }
        }

        public string CartTotalString
        {
            get
            {
                return $"Total: {CartTotal + Tax:C}";
            }
        }

        public decimal CartTotal
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in ProductsInCart)
                {
                    if (item.Product.Bogo)
                    {
                        if (item.Product.Quantity % 2 == 0)
                        {
                            totalPrice += item.Product.Price * (item.Product.Quantity / 2);
                        }
                        else if (item.Product.Quantity % 2 != 0 && item.Product.Quantity > 1)
                        {
                            totalPrice += item.Product.Price * (item.Product.Quantity / 2) + item.Product.Price;
                        }
                        else
                        {
                            totalPrice += item.Product.Price;
                        }
                    }
                    else
                    {
                        totalPrice += item.Product.Price * item.Product.Quantity;
                    }
                }
                //return $"Total: {totalPrice:C}";
                return totalPrice;

            }
        }


        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void PlaceInCart()
        {

            if (ProductToBuy?.Product == null)
            {
                return;
             
            }

            ProductToBuy.Product.Quantity = 1;
            ShoppingCartService.Current.AddToCart(ProductToBuy?.Product, Cart.Id);

            ProductToBuy = null;
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(CartTax));
            NotifyPropertyChanged(nameof(CartSubtotal));

            NotifyPropertyChanged(nameof(CartTotalString));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
