using System.Collections.Generic;
using System.Linq;
using ShoppingKart.Poco;
using ShoppingKart.Repository.Interface;

namespace ShoppingKart.Repository.Fake
{
    public class InventoryRepo : IInventoryRepo
    {
        private static readonly List<Stock> Inventory = new List<Stock>();
        private readonly IProductCatalogueRepo _productCatalogueRepo;
        private readonly object _syncObject = new object();

        public InventoryRepo(IProductCatalogueRepo productCatalogueRepo)
        {
            _productCatalogueRepo = productCatalogueRepo;
            if (Inventory.Count == 0)
            {
                Inventory.AddRange(new[]
                {
                    new Stock {Item = productCatalogueRepo.GetItem('A'), QuantityHeld = 10}
                    , new Stock {Item = productCatalogueRepo.GetItem('B'), QuantityHeld = 10}
                    , new Stock {Item = productCatalogueRepo.GetItem('C'), QuantityHeld = 10}
                    , new Stock {Item = productCatalogueRepo.GetItem('D'), QuantityHeld = 10}
                }
                    );
            }
        }

        public IQueryable<Stock> GetStocks()
        {
            lock (_syncObject)
            {
                return Inventory.AsReadOnly().AsQueryable();
            }
        }

        public Stock GetStock(char sku)
        {
            lock (_syncObject)
            {
                return Inventory.FirstOrDefault(s => s.Item.Sku == sku);
            }
        }

        public bool IsItemInStock(char sku)
        {
            var result = false;
            Stock item = null;
            lock (_syncObject)
            {
                item = Inventory.FirstOrDefault(s => s.Item.Sku == sku);
            }
            if (item != null)
            {
                if (item.QuantityHeld > 0)
                    result = true;
            }
            return result;
        }

        public bool AddStock(char sku, long quantity)
        {
            lock (_syncObject)
            {
                var item = _productCatalogueRepo.GetItem(sku);
                if (item != null)
                {
                    var stock = Inventory.FirstOrDefault(s => s.Item.Sku == sku);
                    if (stock == null)
                    {
                        stock = new Stock {Item = item, QuantityHeld = 0};
                        Inventory.Add(stock);
                    }
                    stock.QuantityHeld += quantity;
                    return true;
                }
            }
            return false;
        }

        public bool DecrementStock(char sku, long quantity)
        {
            var item = _productCatalogueRepo.GetItem(sku);
            lock (_syncObject)
            {
                if (item != null)
                {
                    var stock = Inventory.FirstOrDefault(s => s.Item.Sku == sku);
                    if (stock != null)
                    {
                        if (stock.QuantityHeld >= quantity)
                        {
                            stock.QuantityHeld -= quantity;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}