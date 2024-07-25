using CRM.Library.DTO;
using CRM.Library.Models;
using CRM.Library.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.MAUI.ViewModels
{
    public class SubProductViewModel
    {

        public override string ToString()
        {
            if (Product == null)
            {
                return string.Empty;
            }
            return $"{Product.Id} - {Product.Name} - {Product.Price:C}";
        }
        public ProductDTO? Product { get; set; }
        public int CartID { get; set; }

        public string? Name
        {
            get
            {
                return Product?.Name ?? string.Empty;
            }

            set
            {
                if (Product != null)
                {
                    Product.Name = value;
                }
            }
        }

        public string DisplayPrice
        {
            get
            {
                if (Product == null) { return string.Empty; }

              

                return $"{Product.Price:C}";
            }
        }


        public string PriceAsString
        {
            set
            {
                if (Product == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    Product.Price = price;
                }
                else
                {

                }
            }
        }

        public string SubFrequency { get; set;}

        public int subscribedQuantity { get; set; }

        public SubProductViewModel()
        {
            Product = new ProductDTO();
        }

        public SubProductViewModel(ProductDTO? model)
        {
            if (model != null)
            {
                Product = model;
            }
            else
            {
                Product = new ProductDTO();
            }
        }
        public SubProductViewModel(int productId)
        {
            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == productId);

            if (Product == null)
            {
                Product = new ProductDTO();
            }
            else if (Product != null)
            {
                Product = new ProductDTO(Product);
            }
        }

        public SubProductViewModel(int productId, int cartId)
        {
            CartID = cartId;

            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == productId);
            if (Product == null)
            {
                Product = new ProductDTO();
            }
            else if (Product != null)
            {
                Product = new ProductDTO(Product);
            }
        }

        public void Add()
        {
            if (Product != null)
            {
                Product.Price = Product.Price - (Product.Price * 0.05m);
                Product.Quantity = subscribedQuantity;
                Product.Frequency = SubFrequency;
                ShoppingCartService.Current.AddToCart(Product, CartID);
            }
        }
    }
}
