using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingKart.Poco;
using ShoppingKart.Repository.Interface;

namespace ShoppingKart.Repository.Fake
{
    public class ProductCatalogueRepo : IProductCatalogueRepo
    {
        private static readonly object SyncObject = new object();

        private static readonly List<Item> ProductCatalouge = new List<Item>
        {
            new Item {Sku = 'A', FullRetailPrice = 5.0m}
            ,
            new Item {Sku = 'B', FullRetailPrice = 3.0m}
            ,
            new Item {Sku = 'C', FullRetailPrice = 2.0m}
            ,
            new Item {Sku = 'D', FullRetailPrice = 1.5m}
        };

        public bool ContainsItem(char sku)
        {
            lock (SyncObject)
            {
                return ProductCatalouge.Exists(i => i.Sku == sku);
            }
        }

        public bool AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (item.Sku < 'A' && item.Sku > 'Z')
                throw new ArgumentException("item.Sku Range 'A - Z'");
            lock (SyncObject)
            {
                if (!ProductCatalouge.Exists(i => i.Sku == item.Sku))
                {
                    ProductCatalouge.Add(item);
                    return true;
                }
            }
            return false;
        }

        public bool RemoveItem(char sku)
        {
            lock (SyncObject)
            {
                var item = ProductCatalouge.FirstOrDefault(i => i.Sku == sku);
                if (item != null)
                {
                    ProductCatalouge.Remove(item);
                    return true;
                }
            }
            return false;
        }

        public Item GetItem(char sku)
        {
            lock (SyncObject)
            {
                return ProductCatalouge.FirstOrDefault(i => i.Sku == sku);
            }
        }

        public IQueryable<Item> GetItems()
        {
            lock (SyncObject)
            {
                return ProductCatalouge.AsReadOnly().AsQueryable();
            }
        }
    }
}