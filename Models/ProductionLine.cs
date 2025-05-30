using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class ProductionLine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; } = string.Empty; // "Active" or "Stopped"

        [Range(0.5, 2.0, ErrorMessage = "Efficiency Factor must be between 0.5 and 2.0.")]
        public double EfficiencyFactor { get; set; }

        // Foreign Key - One-to-Many relationship with WorkOrder
        public int? CurrentWorkOrderId { get; set; }  // Optional foreign key
        public WorkOrder? CurrentWorkOrder { get; set; } // Navigation property for the current work order
        // Navigation property (One-to-Many relationship)
        public ICollection<WorkOrder>? WorkOrders { get; set; }
    }
}