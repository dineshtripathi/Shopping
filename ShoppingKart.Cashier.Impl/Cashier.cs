using System.Collections.Generic;
using System.Linq;
using ShoppingKart.Cashier.Interface;
using ShoppingKart.Poco;
using ShoppingKart.Repository.Interface;
using ShoppingKart.RuleEngine.Interface;

namespace ShoppingKart.Cashier.Impl
{
    public class Cashier : ICashier
    {
        private readonly IProductCatalogueRepo _productCatalogue;
        private readonly IInventoryRepo _inventory;
        private readonly IItemOffersRepo _offeresRepo;
        private readonly IRuleEngine _ruleEngine;

        public Cashier(IProductCatalogueRepo productCatalogue, IInventoryRepo inventory, IItemOffersRepo offeresRepo,
            IRuleEngine ruleEngine)
        {
            _productCatalogue = productCatalogue;
            _inventory = inventory;
            _offeresRepo = offeresRepo;
            _ruleEngine = ruleEngine;
        }

        public IEnumerable<BillItem> Checkout(IEnumerable<char> skuEnumerable)
        {
            Dictionary<char, int> itemsCountInBasket = new Dictionary<char, int>();
            Dictionary<char, decimal> itemisedBill = new Dictionary<char, decimal>();
            CreateQuantityPurchasedTable(skuEnumerable, itemsCountInBasket);
            CalculateItemisedTotals(itemisedBill, itemsCountInBasket);
            //
            var billQuery = from cKv in itemsCountInBasket
                join bKv in itemisedBill on cKv.Key equals bKv.Key
                select new BillItem() {Sku = cKv.Key, Quantity = cKv.Value, TotalPrice = bKv.Value};
            return billQuery.AsEnumerable();

        }

        private void CalculateItemisedTotals(Dictionary<char, decimal> itemisedBill, Dictionary<char, int> itemsCountInBasket)
        {
            foreach (var dict in itemsCountInBasket)
            {
                var sku = dict.Key;
                var totalPurchased = dict.Value;
                decimal totalPrice;
                Item item = _productCatalogue.GetItem(sku);
                ItemOffer itemOffer = _offeresRepo.GetOffer(item.Sku);
                if (itemOffer != null)
                {
                    totalPrice = _ruleEngine.GetAmount(itemOffer, totalPurchased);
	                if (totalPrice == 0)
	                {
						totalPrice = totalPurchased * item.FullRetailPrice;
	                }
                    itemisedBill.Add(sku, totalPrice);
                }
                else
                {
                    totalPrice = totalPurchased*item.FullRetailPrice;
                    itemisedBill.Add(sku, totalPrice);
                }
            }
        }

        private void CreateQuantityPurchasedTable(IEnumerable<char> skuEnumerable, Dictionary<char, int> itemsCountInBasket)
        {
            foreach (var sku in skuEnumerable)
            {
                _inventory.DecrementStock(sku, 1);

                if (!itemsCountInBasket.ContainsKey(sku))
                {
                    itemsCountInBasket.Add(sku, 1);
                }
                else
                {
                    int totalPurchased;
                    if (itemsCountInBasket.TryGetValue(sku, out totalPurchased))
                    {
                        itemsCountInBasket[sku] = totalPurchased + 1;
                    }
                }
            }
        }
    }
}
