using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } // Primary Key

        [Required]
        public int UserID { get; set; } // Foreign Key (User)

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime PickupDate { get; set; } // Scheduled pickup time

        public DateTime? DeliveryDate { get; set; } // Nullable

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, In Progress, Completed

        public ICollection<OrderItem> OrderItems { get; set; } // One-to-Many relationship

        [NotMapped]
        public decimal TotalAmount => OrderItems?.Sum(item => item.TotalPrice) ?? 0;
    }

}
