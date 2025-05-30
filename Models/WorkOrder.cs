using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionManagementSystem.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }  // Foreign key to Product
        public Product? Product { get; set; } // Navigation property

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)] // Укажите тип данных Date для поля даты
        public DateTime StartDate { get; set; }

        public DateTime EstimatedEndDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = string.Empty; //"Pending"/ "InProgress"/ "Completed"/ "Cancelled"

        public int? ProductionLineId { get; set; } // Foreign key to ProductionLine (nullable)
        public ProductionLine? ProductionLine { get; set; } // Navigation property

        // Дополнительное свойство для хранения времени производства продукта
        public int ProductionTimePerUnit { get; set; }
    }
}