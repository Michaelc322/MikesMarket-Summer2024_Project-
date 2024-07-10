﻿using CRM.Library.Models;

namespace CRM.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product>? Contents { get; set; }

        public ShoppingCart()
        {
            Contents = new List<Product>();
        }
    }
}
