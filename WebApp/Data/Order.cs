using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        [StringLength(450)]
        public string? AccountId { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public int QuanlityTotal { get; set; }
        public int Total_Price { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string? Shipping_Address { get; set; }
        public string? Email_User { get; set; }
        public string? Status { get; set; }

        public ICollection<Product_Order> Product_Orders { get; set; }

    }
}
