using ProductionManagementSystem.Data;
using ProductionManagementSystem.Services;

public class ProductionCalculationService : IProductionCalculationService
{
    private readonly ApplicationDbContext _context;

    public ProductionCalculationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public double CalculateProductionTime(int productId, int quantity, int? productionLineId)
    {
        var product = _context.Products.Find(productId);
        if (product == null)
        {
            throw new ArgumentException("Invalid product ID");
        }

        // Получаем коэффициент эффективности линии
        double efficiencyFactor = 1.0; // По умолчанию
        if (productionLineId.HasValue)
        {
            var line = _context.ProductionLines.Find(productionLineId.Value);
            if (line != null)
            {
                efficiencyFactor = line.EfficiencyFactor;
            }
        }

        double productionTimeInMinutes = (quantity * product.ProductionTimePerUnit) / efficiencyFactor;
        return productionTimeInMinutes;
    }
}