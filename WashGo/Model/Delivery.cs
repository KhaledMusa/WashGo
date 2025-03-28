using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashGo.Model
{
    public class Delivery
    {
        [Key]
        public int DeliveryID { get; set; } // Primary Key

        [Required]
        public int OrderID { get; set; } // Foreign Key (Order)

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [Required]
        public int DeliveryPersonID { get; set; } // Foreign Key (Delivery Person)

        [ForeignKey("DeliveryPersonID")]
        public User DeliveryPerson { get; set; } // Assuming delivery person is a User

        [Required]
        public DateTime PickupTime { get; set; } // When clothes were picked up

        public DateTime? DeliveryTime { get; set; } // When clothes were delivered

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Assigned"; // Assigned, PickedUp, Delivered
    }
}
