using System.Collections.Generic;
using System.ComponentModel;
using ShoppingKart.Poco;

namespace ShoppingKart.ShoppingBasket.Interface
{
    public interface IShoppingBasket
    {
        IEnumerable<char> GetItems();
        bool AddItem(char sku,long quantity);
        bool UpdateItem(char sku, long quantity);
        bool RemoveItem(char sku);
    }
}
