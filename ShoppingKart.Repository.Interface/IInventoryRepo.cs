using System.Linq;
using ShoppingKart.Poco;

namespace ShoppingKart.Repository.Interface
{
    public interface IInventoryRepo
    {
        IQueryable<Stock> GetStocks();
        Stock GetStock(char sku);
        bool IsItemInStock(char sku);
        bool AddStock(char sku, long quantity);
        bool DecrementStock(char sku, long quantity);
    }
}
