using SortedKata.BLL.Models;

namespace SortedKata.BLL.Interfaces
{
    public interface IItemOrchestrator
    {
        bool AddItem(Item item);
        Item GetItem(string sku);
    }
}
