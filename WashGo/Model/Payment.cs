using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; } // Primary Key

        [Required]
        public int OrderID { get; set; } // Foreign Key (Order)

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } // Cash, Credit Card, PayPal

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsPaid { get; set; } = false; // Default unpaid
    }
}

