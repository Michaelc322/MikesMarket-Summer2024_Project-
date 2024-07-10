using CRM.Library.Models;
using CRM.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.MAUI.ViewModels
{
    public class ProductConfigViewModel
    {

        public override string ToString()
        {
            if (Product == null)
            {
                return string.Empty;
            }
            return $"{Product.Id} - {Product.Name} - {Product.Price:C}";
        }
        public Product? Product { get; set; }

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

        public string MarkdownPrice
        {
            set
            {
                if (Product == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    if(Product.Price < price)
                    {
                        price = Product.Price;
                    }
                    Product.Markdown = price;
                }
                else
                {

                }
            }
        }

        public ProductConfigViewModel()
        {
            Product = new Product();
        }

        public ProductConfigViewModel(int productId)
        {

            Product = InventoryServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == productId);
            if (Product == null)
            {
                Product = new Product();
            }
        }
        public ProductConfigViewModel(Product? model)
        {
            if (model != null)
            {
                Product = model;
            }
            else
            {
                Product = new Product();
            }
        }

        public void Add()
        {
            if (Product != null)
            {
                InventoryServiceProxy.Current.AddOrUpdate(Product);
            }
        }
    }
}
