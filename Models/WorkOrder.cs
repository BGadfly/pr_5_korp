using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }  // Foreign key to Product
        public Product? Product { get; set; } // Navigation property

        public int? ProductionLineId { get; set; } // Foreign key to ProductionLine (nullable)
        public ProductionLine? ProductionLine { get; set; } // Navigation property

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        public DateTime EstimatedEndDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty; //"Pending"/ "InProgress"/ "Completed"/ "Cancelled"
    }
}
