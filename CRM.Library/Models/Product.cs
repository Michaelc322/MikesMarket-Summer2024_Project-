
namespace CRM.Library.Models
{
        public class Product
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public int Id { get; set; }

            public int Quantity { get; set; }

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

            public override string ToString()
            {
                return $"[{Id}] {Name} - {Description} - ${Price}\nQuantity: {Quantity}";
            }


        }
}
