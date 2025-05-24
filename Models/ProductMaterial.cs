using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductionManagementSystem.Models
{
    public class ProductMaterial
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int MaterialId { get; set; }

        public decimal QuantityNeeded { get; set; }

        public Product? Product { get; set; } // Navigation property
        public Material? Material { get; set; } // Navigation property
    }
}
