using CRM.Library.DTO;
using CRM.Library.Models;
using CRM.Library.Models;
using eCommerce.API.Database;
using System.Linq;

namespace eCommerce.API.EC
{
    public class InventoryEC
    {

        public InventoryEC()
        {
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return Filebase.Current.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<IEnumerable<ProductDTO>> Search(string? query)
        {
            return Filebase.Current.Products.Where(p =>
            (p?.Name != null && p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty))
                ||
            (p?.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
                .Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            return new ProductDTO(Filebase.Current.Delete(id));

        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {

            return new ProductDTO(Filebase.Current.AddOrUpdate(new Product(p)));

        }
    }
}