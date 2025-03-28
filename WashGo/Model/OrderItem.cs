using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; } // Primary Key

        [Required]
        public int OrderID { get; set; } // Foreign Key referencing Order

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [Required]
        [StringLength(100)]
        public string ItemType { get; set; } // e.g., Shirt, Suit, Blanket

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } // Price per unit

        [NotMapped]
        public decimal TotalPrice => Price * Quantity; // Computed property (not stored in DB)
    }
}

