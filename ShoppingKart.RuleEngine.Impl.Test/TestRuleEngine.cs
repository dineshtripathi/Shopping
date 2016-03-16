using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingKart.Poco;
using ShoppingKart.RuleEngine.Interface;
using Moq;

namespace ShoppingKart.RuleEngine.Impl.Test
{
	[TestClass]
	public class TestRuleEngine
	{
		[TestMethod]
		public void ToGetAmountWithoutAnyPurchase()
		{
			IRuleEngine ruleengine = new RuleEngine();
			var mockItem = Mock.Of<Item> ( s => s.FullRetailPrice == 5.0m
												&& s.Sku == 'A' );
			long result;
			var mockOffer = Mock.Of<Offer> ( m => m.GetDiscountedAmount ( 0, out result ) == 0.0m );
			var mockItemOffer = Mock.Of<ItemOffer> ( m => m.Offer == mockOffer && m.Item == mockItem );
			decimal actual = ruleengine.GetAmount ( mockItemOffer, 0 );
			Assert.AreEqual ( actual, 0m );
		}

		[TestMethod]
		public void ToGetItemPriceForOneQuantity()
		{
			IRuleEngine ruleengine = new RuleEngine();
			var mockItem = Mock.Of<Item> ( s => s.FullRetailPrice == 5.0m
												&& s.Sku == 'A' );
			long result;
			var mockOffer = Mock.Of<Offer> ( m => m.GetDiscountedAmount ( 1, out result ) == 5.0m );
			var mockItemOffer = Mock.Of<ItemOffer> ( m => m.Offer == mockOffer && m.Item == mockItem );
			decimal actual = ruleengine.GetAmount ( mockItemOffer, 1 );
			Assert.AreEqual ( actual, 5.0m );
		}

		[TestMethod]
		public void ToGetItemPriceForTwoQuantity()
		{
			IRuleEngine ruleengine = new RuleEngine();
			var mockItem =  Mock.Of<Item>(s=> s.FullRetailPrice == 5.0m 
												&& s.Sku == 'A');
			long result;
			var mockOffer =  Mock.Of<Offer>( m => m.GetDiscountedAmount(2, out result) == 7.5m );
			var mockItemOffer = Mock.Of<ItemOffer>(m => m.Offer == mockOffer && m.Item == mockItem);
			decimal actual = ruleengine.GetAmount ( mockItemOffer, 2 );
			Assert.AreEqual ( actual, 7.5m );
		}

		[TestMethod]
		[ExpectedException ( typeof ( System.ArgumentNullException ) )]
		public void ToCheckWhetherTheOfferIsNull( )
		{
			IRuleEngine ruleengine = new RuleEngine();
			var x = ruleengine.GetAmount(null, 0);
			
		}

		[TestMethod]
		[ExpectedException ( typeof ( System.ArgumentException ) )]
		public void ToCheckWhetherTheItemOfferMemberAreNull()
		{
			IRuleEngine ruleengine = new RuleEngine();
			var itemOffer = new ItemOffer();
			var x = ruleengine.GetAmount ( itemOffer, 0 );
		}
	}
}
