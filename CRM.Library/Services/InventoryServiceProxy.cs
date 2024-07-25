using CRM.Library.DTO;
using CRM.Library.Models;
using CRM.Library.Utilities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;

namespace CRM.Library.Services
{
    public class InventoryServiceProxy
    {
        //private List<Product>? products;
        private InventoryServiceProxy() {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
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

         


       /* public ReadOnlyCollection<Product>? Products
        {
            get
            {
                return products?.AsReadOnly();
            }
        }*/

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
        {
            get
            {
                return products.AsReadOnly();
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

        /*
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
        */

        public async Task<ProductDTO> ConfigureProduct(bool isBogo, decimal Markdown, ProductDTO p)
        {
            if (products == null)
            {
                return null;
            }

            p.Markdown = Markdown;
            p.Bogo = isBogo;

            p = await AddOrUpdate(p);

            return p;
        }

        /*
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
        */

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);
            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            var result = await new WebRequestHandler().Post("/Inventory", p);
            return JsonConvert.DeserializeObject<ProductDTO>(result);
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            var response = await new WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<ProductDTO>(response);
            return itemToDelete;
        }

        /* USED IN CONSOLE APPLICATION
        public Product? GetItemByID(int id)
        {
            if (products == null)
            {
                return null;
            }

            return products?.FirstOrDefault(i => i.Id == id);
        }
        */
        public async Task<IEnumerable<ProductDTO>> Search(Query? query)
        {
            if (query == null || string.IsNullOrEmpty(query.QueryString))
            {
                return await Get();
            }

            var result = await new WebRequestHandler().Post("/Inventory/Search", query);
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(result) ?? new List<ProductDTO>();
            return Products;
        }

        public decimal taxAmount { get; set; } = 0;

        public void ChangeTax(decimal taxRate)
        {
            taxAmount = taxRate;
        }

    }
}
