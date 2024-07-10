

using CRM.Library.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;

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

        public ShoppingCart? Subscriptions { get; set;}



        public ShoppingCart Cart
        {
            get
            {
                if (!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
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

        public int LastId
        {
            get
            {
                if (carts?.Any() ?? false)
                {
                    return carts?.Select(i => i.Id)?.Max() ?? 0;
                }

                return 0;
            }
        }
        public ShoppingCart AddOrUpdate(ShoppingCart c)
        {
            if (carts == null)
            {
                return null;
            }

            bool isAdd = false;

            if (c.Id == 0)
            {
                c.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                carts.Add(c);
            }

            return c;
        }

        public void AddToCart(Product newProduct, int Id)
        {

            ShoppingCart AssignedCart = carts.FirstOrDefault(c => c.Id == Id);

            if (AssignedCart == null || AssignedCart.Contents == null)
            {
                return;
            }

            var existingProduct = AssignedCart?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if (inventoryProduct == null)
            {
                return;
            }

            if(newProduct.Quantity > inventoryProduct.Quantity)
            {
                newProduct.Quantity = inventoryProduct.Quantity;
            }

            inventoryProduct.Quantity -= newProduct.Quantity;

            if (inventoryProduct.Quantity < 0)
            {
                inventoryProduct.Quantity = 0;
                return;
            }

            if (newProduct.Markdown > 0)
            {
                newProduct.Price = newProduct.Price -= newProduct.Markdown;
            }

            if (existingProduct != null)
            {
                if (existingProduct.Bogo && (existingProduct.Quantity + newProduct.Quantity) % 2 == 0)
                {
                    newProduct.Price = 0;
                    existingProduct.Quantity += newProduct.Quantity;

                    return;
                }

                if(existingProduct.Frequency != newProduct.Frequency)
                {
                    existingProduct.Frequency = newProduct.Frequency;
                }
                // update
                existingProduct.Quantity += newProduct.Quantity;
            }
            else
            {
                //add
                AssignedCart?.Contents?.Add(newProduct);
                
            }
        }

        public void AddSubscription(Product newProduct)
        {
            if (Cart == null || Cart.Contents == null)
            {
                return;
            }

            var existingProduct = Cart?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if (inventoryProduct == null)
            {
                return;
            }

            inventoryProduct.Quantity -= newProduct.Quantity;

            if (inventoryProduct.Quantity < 0)
            {
                inventoryProduct.Quantity = 0;
                return;
            }

            if (newProduct.Markdown > 0)
            {
                newProduct.Price = newProduct.Price -= newProduct.Markdown;
            }

            if (existingProduct != null)
            {
                if (existingProduct.Bogo && (existingProduct.Quantity + newProduct.Quantity) % 2 == 0)
                {
                    newProduct.Price = 0;
                    existingProduct.Quantity += newProduct.Quantity;

                    return;
                }
                // update
                existingProduct.Quantity += newProduct.Quantity;
            }
            else
            {
                //add
                Cart?.Contents?.Add(newProduct);

            }
        }

    }

}

