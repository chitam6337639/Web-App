using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string? ImageURL { get; set; }
        public string? ProductDescription { get; set; }

        public Category? Category { get; set; }

        public ICollection<Product_Order>? Product_Orders { get; set; }

       
    }
}
