using CRM.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Library.DTO
{
    public class ProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Markdown { get; set; } = 0;

        public bool Bogo { get; set; } = false;
        public string Frequency { get; set; } = "None";


        public ProductDTO(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Markdown = p.Markdown;
            Bogo = p.Bogo;
            if (!string.IsNullOrEmpty(p.Frequency))
            {
                Frequency = p.Frequency;
            }
            else
            {
                Frequency = "None";
            }


        }

        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Markdown = p.Markdown;
            Bogo = p.Bogo;

            if (!string.IsNullOrEmpty(p.Frequency)){
                Frequency = p.Frequency;
            }
            else
            {
               Frequency = "None";
            }
        }

        public ProductDTO() { }
    }
}