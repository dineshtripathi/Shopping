using System;
using System.Collections.Generic;
using ShoppingKart.ShoppingBasket.Interface;

namespace ShoppingKart.ShoppingBasket.Fake
{
    public class FakeShoppingBasket:IShoppingBasket
    {
        private readonly Dictionary<char,long> _shoppingBasket = new Dictionary<char, long>();
        public IEnumerable<char> GetItems()
        {
            foreach (var kv in _shoppingBasket)
            {
                for(var i = 0; i< kv.Value;i++)
                    yield return kv.Key;
            }

        }

        public bool AddItem(char sku, long quantity)
        {
            //need to validate sku, but have skipped that for to simplyfy the implementation
            if(quantity <=0)
                throw new ArgumentException("quantity must be > 0");
            long currentQty = 0;
            
            if (!_shoppingBasket.TryGetValue(sku, out currentQty))
            {
                _shoppingBasket.Add(sku, quantity);
                return true;
            }
            return UpdateItem(sku, quantity + currentQty);
        }

        public bool UpdateItem(char sku, long quantity)
        {
            //need to validate sku, but have skipped that for to simplyfy the implementation
            if (quantity <= 0)
                throw new ArgumentException("quantity must be > 0");
            if (_shoppingBasket.ContainsKey(sku))
            {
                _shoppingBasket[sku] = quantity;
                return true;
            }
            return false;
        }

        public bool RemoveItem(char sku)
        {
            if (_shoppingBasket.ContainsKey(sku))
            {
                _shoppingBasket.Remove(sku);
                return true;
            }
            return false;
        }
    }
}
