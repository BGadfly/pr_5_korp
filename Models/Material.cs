using System.ComponentModel.DataAnnotations;

namespace ProductionManagementSystem.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        public decimal Quantity { get; set; }

        [Required(ErrorMessage = "Unit of Measure is required")]
        public string UnitOfMeasure { get; set; } = string.Empty;

        public decimal MinimalStock { get; set; }

        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
