using CRM.Library.Models;
using System.Collections.ObjectModel;

namespace CRM.Library.Services
{
    public class ShopServiceProxy
    {
        private List<Product>? items;

        private ShopServiceProxy()
        {
            items = new List<Product>();
        }

        public ReadOnlyCollection<ShoppingCart>? carts;


        private static ShopServiceProxy? instance;
        private static object instanceLock = new object();
        public static ShopServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShopServiceProxy();
                    }

                    return instance;
                }
            }
        }

        public ReadOnlyCollection<Product>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        // functionality

        public int LastId
        {
            get
            {
                if (items?.Any() ?? false)
                {
                    return items?.Select(i => i.Id)?.Max() ?? 0;
                }

                return 0;
            }
        }

        public Product? AddToCart(Product item)
        {
            if (items == null)
            {
                return null;
            }


            items.Add(item);
            return item;
        }

        public bool? RemoveFromCart(int id)
        {
            if (items == null)
            {
                return false;
            }

            var itemToDelete = items?.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                itemToDelete.Quantity--;
                

                if(itemToDelete.Quantity == 0)
                {
                    items?.Remove(itemToDelete);
                    return true;
                }

                return true;
            }
            else
            {
                Console.WriteLine("Item not found in the cart!");
                return false;
            }


        }

        public Product? GetItemByID(int id)
        {
            if (items == null)
            {
                return null;
            }

            return items?.FirstOrDefault(i => i.Id == id);
        }

    }
}
