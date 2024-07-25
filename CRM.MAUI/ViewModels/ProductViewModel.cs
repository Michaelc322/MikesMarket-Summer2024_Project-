using CRM.Library.DTO;
using CRM.Library.Models;
using CRM.Library.Services;
using System.Windows.Input;

using Product = CRM.Library.Models.Product;

namespace CRM.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public ICommand? DeleteCommand { get; private set; }
        public ICommand? EditCommand   { get; private set; }

        public override string ToString()
        {
            if (Product == null)
            {
                return string.Empty;
            }
            return $"{Product.Id} - {Product.Name} - {Product.Price:C}";
        }
        public ProductDTO? Product { get; set; }

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

        public string MarkdownSale
        {
            get
            {
               if (Product?.Markdown > 0)
               {
                   return $"SALE: -{Product.Markdown:C}";
               }

                return string.Empty;
            }
        }

        public string isBogo
        {
            get
            {
                if (Product?.Bogo == true)
                {
                    return "BOGO DEAL";
                }

                return string.Empty;
            }
        }

        public ProductViewModel()
        {
            Product = new ProductDTO();
            SetupCommands();
        }


        public ProductViewModel(int productId = 0)
        {
            if (productId == 0)
            {
                Product = new ProductDTO();
            }
            else
            {
                Product = InventoryServiceProxy
                    .Current
                    .Products.FirstOrDefault(p => p.Id == productId)
                    ?? new ProductDTO();
            }
        }

        public ProductViewModel(ProductDTO? model)
        {
            if (model != null)
            {
                Product = model;
                SetupCommands();
            }
            else
            {
                Product = new ProductDTO();
                SetupCommands();

            }
        }

        public void SetupCommands()
        {
            DeleteCommand = new Command(
               (p) => ExecuteDelete((p as ProductViewModel)?.Product?.Id));

            EditCommand = new Command(
                (p) => ExecuteEdit((p as ProductViewModel)));
        }

        public async void Add()
        {
            if (Product != null)
            {
               Product = await InventoryServiceProxy.Current.AddOrUpdate(Product);
            }
        }

        private void ExecuteEdit(ProductViewModel? p)
        {
            if (p?.Product == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?productId={p.Product.Id}");
        }

        private async void ExecuteDelete(int? id)
        {
            if (id == null)
            {
                return;
            }

            Product = await InventoryServiceProxy.Current.Delete(id ?? 0);
            
        }
    }
}
