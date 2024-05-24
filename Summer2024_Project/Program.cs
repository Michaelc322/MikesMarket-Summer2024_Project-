using CRM.Library.Services;
using CRM.Models;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace Summer2024_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool start = true;
            while (start)
            {
                Console.WriteLine("Welcome Home");
                Console.WriteLine("------------");
                Console.WriteLine("Select an option:");
                Console.WriteLine("(1) Inventory Manager");
                Console.WriteLine("(2) Shop");
                Console.WriteLine("(3) Exit");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        InventoryManager();
                        break;
                    case "2":
                        Shop();
                        break;
                    case "3":
                        start = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                
            }
        }

        public static void InventoryManager() {
            var itemService = ItemServiceProxy.Current;

            while (true)
            {
                Console.WriteLine("Inventory Management:");
                Console.WriteLine("(1) View Inventory");
                Console.WriteLine("(2) Add Product");
                Console.WriteLine("(3) Update Product");
                Console.WriteLine("(4) Remove Product");
                Console.WriteLine("(5) Return to Main Menu");
                Console.WriteLine();

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewInventory();
                        break;
                    case "2":
                        AddProduct();
                        break;
                    case "3":
                        UpdateProduct();
                        break;
                    case "4":
                        RemoveProduct();
                        break;
                    case "5":
                        return; // This will return to the main menu
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }

            void AddProduct() { 
                Console.WriteLine("Enter the name of the product:");
                var name = Console.ReadLine();
                Console.WriteLine("Enter the description of the product:");
                var description = Console.ReadLine();
                Console.WriteLine("Enter the price of the product:");
                var price = Console.ReadLine();
                Console.WriteLine("Enter the quantity of the product:");
                var quantity = Console.ReadLine();

                if (quantity == null) { 
                    Console.WriteLine("Invalid quantity, please try again.");
                    return;
                }

                if(double.TryParse(price, out var priceValue))
                {
                    itemService.AddOrUpdate(new Item { Name = name, Description = description, Price = priceValue, Quantity = int.Parse(quantity)});
                }
                else
                {
                    Console.WriteLine("Invalid price, please try again.");
                }
            }

            void ViewInventory() {
                Console.WriteLine();
                Console.WriteLine("Inventory:");
                itemService?.Items?.ToList()?.ForEach(Console.WriteLine);
                Console.WriteLine();
            }

            void UpdateProduct()
            {
                Console.WriteLine("Enter the ID of the product to update:");
                var id = Console.ReadLine();

                if (int.TryParse(id, out var idValue))
                {
                    var item = itemService.GetItemByID(idValue);

                    if (item == null)
                    {
                        Console.WriteLine("Item not found in inventory!");
                        return;
                    }

                    Console.WriteLine("Enter the updated name of the product:");
                    var name = Console.ReadLine();
                    Console.WriteLine("Enter the updated description of the product:");
                    var description = Console.ReadLine();
                    Console.WriteLine("Enter the updated price of the product:");
                    var price = Console.ReadLine();
                    Console.WriteLine("Enter the updated quantity of the product:");
                    var quantity = Console.ReadLine();

                    if (quantity == null)
                    {
                        Console.WriteLine("Invalid quantity, please try again.");
                        return;
                    }

                    if (double.TryParse(price, out var priceValue))
                    {
                        item.Name = name;
                        item.Description = description;
                        item.Price = priceValue;
                        item.Quantity = int.Parse(quantity);

                        Console.WriteLine();
                        Console.WriteLine("Product updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid price, please try again.");
                    }
                }
            }

            void RemoveProduct()
            {
                Console.WriteLine("Enter the ID of the product to remove:");
                var id = Console.ReadLine();

                if (int.TryParse(id, out var idValue))
                {
                    itemService.Delete(idValue);
                }
                else
                {
                    Console.WriteLine("Invalid ID, please try again.");
                }
            }
        }

        public static void Shop()
        {
            List<Item> cart = new List<Item>();
            var itemService = ItemServiceProxy.Current;
            var shopService = ShopServiceProxy.Current;



            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Select an option:");
                Console.WriteLine("(1) Place item in cart");
                Console.WriteLine("(2) Remove an item from cart");
                Console.WriteLine("(3) View Cart");
                Console.WriteLine("(4) Checkout");
                Console.WriteLine("(5) Return to Main Menu");
                Console.WriteLine();

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddItemToCart();
                        break;
                    case "2":
                        RemoveFromCart();
                        break;
                    case "3":
                        shopService?.Items?.ToList()?.ForEach(Console.WriteLine);
                        break;
                    case "4":
                        Checkout();
                        return;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                void AddItemToCart() { 
                    Console.WriteLine("Enter the ID of the item you would like to add to your cart:");
                    var id = Console.ReadLine();
                    bool add = true;
                    List<Item> items = shopService?.Items?.ToList() ?? new List<Item>();

                    if(int.TryParse(id, out var idValue)){ 
                        foreach(var item in items)
                        {
                            if(item.Id == idValue)
                            {
                                if(itemService?.GetItemByID(idValue)?.Quantity == 0)
                                {
                                    Console.WriteLine("Item is out of stock!");
                                    add = false;
                                    return;
                                }
                                else
                                {
                                    var inventoryItem = itemService?.GetItemByID(idValue)?.Quantity;
                                    inventoryItem--;
                                    item.Quantity++;
                                    Console.WriteLine("Item added to cart!");
                                    add = false;
                                    return;
                                }

                            }
                        }

                        if (add)
                        {
                            var addItem = itemService.GetItemByID(idValue);

                            if(addItem == null)
                            {
                                Console.WriteLine("Item not found in inventory!");
                                return;
                            }
                            var addedItem = shopService?.AddToCart(new Item
                            {
                                Name = addItem.Name,
                                Description = addItem.Description,
                                Price = addItem.Price,
                                Id = addItem.Id,
                                Quantity = 1
                            });


                            addItem.Quantity--;
                            Console.WriteLine();
                            Console.WriteLine("Item added to cart!");
                            Console.WriteLine();
                        }
                    }
                    else{
                        Console.WriteLine("Invalid ID, please try again.");
                    }
                }

                void RemoveFromCart() {
                    var cartItems = shopService?.Items?.ToList();
                    if (cartItems == null)
                    {
                        Console.WriteLine("Cart is empty!");
                    }
                    Console.WriteLine("ID of the item you would like to remove from your cart:");
                    var id = Console.ReadLine();

                    if (int.TryParse(id, out var idVal))
                    {
                        bool removed = shopService?.RemoveFromCart(idVal) ?? false;

                        if (removed)
                        {
                            Console.WriteLine("Item removed from cart!");
                            var itemQuantity = itemService.GetItemByID(idVal)?.Quantity;
                            itemQuantity++;
                        }
                    }
                }

                void Checkout()
                {
                    var items = shopService?.Items?.ToList() ?? new List<Item>();
                    Console.WriteLine("Thank you for shopping with us!");
                    Console.WriteLine("Here is your receipt:");

                    Console.WriteLine("---------------------");
                    Console.WriteLine("Items Purchased:");
                    Console.WriteLine("---------------------");


                    double total = 0;

                    foreach (var item in items)
                    {
                        if (item.Quantity > 1)
                        {
                            for (int i = 0; i < item?.Quantity; i++)
                            {
                                Console.WriteLine(item.Name + $"    ${item?.Price}");
                                total += item?.Price ?? 0;
                            }
                        }
                        else { 
                            Console.WriteLine(item.Name + $"    ${item?.Price}");
                            total += item?.Price ?? 0;
                        }
                    }


                    Console.WriteLine();
                    Console.WriteLine("---------------------");
                    Console.WriteLine($"Subtotal: ${total}");
                    Console.WriteLine($"Tax: ${total * 0.07}");
                    Console.WriteLine($"Total: ${total * 1.07}");
                    Console.WriteLine("---------------------");



                }
            }

        }

    }
}
