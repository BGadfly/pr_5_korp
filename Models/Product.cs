using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty; //Чтобы избежать null reference warnings

        public string? Description { get; set; }

        public string? Specifications { get; set; } // JSON в виде строки

        public string? Category { get; set; }

        public int MinimalStock { get; set; }

        public int ProductionTimePerUnit { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
