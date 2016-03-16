using System.Linq;
using ShoppingKart.Poco;

namespace ShoppingKart.Repository.Interface
{
    public interface IProductCatalogueRepo
    {
        bool ContainsItem(char sku);
        bool AddItem(Item item);
        bool RemoveItem(char sku);
        Item GetItem(char sku);
        IQueryable<Item> GetItems();
    }
}
