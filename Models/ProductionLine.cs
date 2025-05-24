using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class ProductionLine
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty; //"Active"/"Stopped"

        [Range(0.5, 2.0, ErrorMessage = "Efficiency Factor must be between 0.5 and 2.0")]
        public float EfficiencyFactor { get; set; }

        public int? CurrentWorkOrderId { get; set; }  // Foreign key to WorkOrder
        public WorkOrder? CurrentWorkOrder { get; set; } // Navigation property

        public ICollection<WorkOrder>? WorkOrders { get; set; } // Navigation property

    }
}
