

using CRM.Library.Models;
using System.Collections.ObjectModel;

namespace CRM.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance;
        private static object instanceLock = new object();

        private List<ShoppingCart>? carts;

        public ReadOnlyCollection<ShoppingCart>? Carts
        {
            get
            {
                return carts?.AsReadOnly();
            }
        }


        public ShoppingCart Cart
        {
            get
            {
                if (carts == null || !carts.Any())
                {
                    carts = new List<ShoppingCart>();
                    carts.Add(new ShoppingCart());
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartService() {
            carts = new List<ShoppingCart>();
        }

        public static ShoppingCartService Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public ShoppingCart AddOrUpdate(ShoppingCart c)
        {
            carts?.Add(c);

            return c;
        }

        public void AddToCart(Product newProduct)
        {
            if (Cart == null || Cart.Contents == null)
            {
                return;
            }

            Product newItem = new Product { Id = newProduct.Id, Name = newProduct.Name, Description = newProduct.Description, Price = newProduct.Price, Quantity = newProduct.Quantity }; 

            var existingProduct = Cart?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newItem.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newItem.Id);
            if (inventoryProduct == null)
            {
                return;
            }

            inventoryProduct.Quantity -= newItem.Quantity;

            if (existingProduct != null)
            {
                // update
                existingProduct.Quantity += newItem.Quantity;
            }
            else
            {
                //add
                Cart?.Contents.Add(newItem);
                
            }
        }

    }

}

