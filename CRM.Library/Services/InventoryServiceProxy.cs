using CRM.Library.Models;
using System.Collections.ObjectModel;

namespace CRM.Library.Services
{
    public class InventoryServiceProxy
    {
        private List<Product>? products;
        private InventoryServiceProxy() {
            products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 3 },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.0m, Quantity = 5 },
            };
        }

        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }

                    return instance;
                }
            }
        }


        public ReadOnlyCollection<Product>? Products
        {
            get
            {
                return products?.AsReadOnly();
            }
        }

        // functionality

        public int LastId
        {
            get
            {
                if (products?.Any() ?? false) { 
                    return products?.Select(i => i.Id)?.Max() ?? 0;
                }

                return 0;
            }
        }

        public Product? AddOrUpdate(Product p)
        {
            if(products == null)
            {
                return null;
            }

            bool isAdd = false;

            if(p.Id == 0)
            {
                p.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                products.Add(p);
            }

            return p;
        }

        public void Delete(int id)
        {
            if (products == null)
            {
                return;
            }

            var itemToDelete = products?.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                products?.Remove(itemToDelete);
            }
            else
            {
               Console.WriteLine("Item not found in the inventory!");
            }


        }

        public Product? GetItemByID(int id)
        {
            if (products == null)
            {
                return null;
            }

            return products?.FirstOrDefault(i => i.Id == id);
        }

    }
}
