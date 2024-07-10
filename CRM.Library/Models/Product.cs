
namespace CRM.Library.Models
{
        public class Product
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int Id { get; set; }

            public int Quantity { get; set; }

            public decimal Markdown { get; set; } = 0;

            public bool Bogo { get; set; }

            public string Frequency { get; set; }

            

            public string? Display
            {
                get
                {
                    return ToString();
                }
            }

            public Product()
            {

            }

        public Product(Product p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Markdown = p.Markdown;
            Bogo = p.Bogo;
            Frequency = p.Frequency;
        }

        public Product(Product p, bool isBogo)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            Markdown = p.Markdown;
            Bogo = isBogo;
        }

        public override string ToString()
            {
                return $"[{Id}] {Name} - {Description} - ${Price}\nQuantity: {Quantity}";
            }


        }
}
