
namespace SortedKata.BLL.Interfaces
{
    public interface ICheckoutOrchestrator
    {
        bool ScanItem(string sku);
        decimal GetTotalPrice();
        decimal CalculateDiscount(string sku);
    }
}
