using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Unit of measure is required.")]
        public string UnitOfMeasure { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Minimal stock must be a non-negative number.")]
        public decimal MinimalStock { get; set; }

        // Navigation property (если у вас есть связь с другими сущностями)
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}