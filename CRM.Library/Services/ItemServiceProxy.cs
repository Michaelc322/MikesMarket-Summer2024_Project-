using CRM.Models;
using System.Collections.ObjectModel;

namespace CRM.Library.Services
{
    public class ItemServiceProxy
    {
        private ItemServiceProxy() {
            items = new List<Item>();
        }

        private static ItemServiceProxy? instance;
        private static object instanceLock = new object();
        public static ItemServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemServiceProxy();
                    }

                    return instance;
                }
            }
        }

        private List<Item>? items;

        public ReadOnlyCollection<Item>? Items
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
                if (items?.Any() ?? false) { 
                    return items?.Select(i => i.Id)?.Max() ?? 0;
                }

                return 0;
            }
        }

        public Item? AddOrUpdate(Item item)
        {
            if(items == null)
            {
                return null;
            }

            var isAdd = false;

            if(item.Id == 0)
            {
                item.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                items.Add(item);
            }

            return item;
        }

        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }

            var itemToDelete = items?.FirstOrDefault(i => i.Id == id);

            if (itemToDelete != null)
            {
                items?.Remove(itemToDelete);
            }
            else
            {
               Console.WriteLine("Item not found in the inventory!");
            }


        }

        public Item? GetItemByID(int id)
        {
            if (items == null)
            {
                return null;
            }

            return items?.FirstOrDefault(i => i.Id == id);
        }

    }
}
