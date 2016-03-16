using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ShoppingKart.Cashier.Impl.Test
{
	using ShoppingKart.Cashier.Interface;
	using ShoppingKart.Poco;
	using ShoppingKart.Repository.Fake;
	using ShoppingKart.Repository.Interface;
	using ShoppingKart.RuleEngine.Interface;


	[TestClass]
	public class CashierTest
	{
		[TestMethod]
		public void ToTestCheckout()
		{

			var productCatalog = new ProductCatalogueRepo();
			productCatalog.AddItem ( new Item { Sku = 'A', FullRetailPrice = 5.0m } );
			productCatalog.AddItem ( new Item { Sku = 'B', FullRetailPrice = 3.0m } );
			productCatalog.AddItem ( new Item { Sku = 'C', FullRetailPrice = 2.0m } );

			var inventoryRepo = new InventoryRepo ( productCatalog );
			var itemOfferRepo = new ItemOffersRepo ( productCatalog );
		
			var ruleEngine = Mock.Of<IRuleEngine> ();

			var cashier = new Cashier(productCatalog, inventoryRepo, itemOfferRepo, ruleEngine);

			IEnumerable<char> purchaseItems = new List<char>() {'A', 'B', 'C'};
			var billItems = cashier.Checkout(purchaseItems);

			decimal total = 0;
			foreach (var b in billItems)
			{
				total += b.TotalPrice;
			}
			Assert.AreEqual ( total, 10.0m );
		}
	}
}
