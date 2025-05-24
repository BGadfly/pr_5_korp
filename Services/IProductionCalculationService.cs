namespace ProductionManagementSystem.Services
{
    public interface IProductionCalculationService
    {
        double CalculateProductionTime(int productId, int quantity, int? productionLineId);
    }
}