using System;
using System.Linq;
using ShoppingKart.Cashier.Interface;
using ShoppingKart.Repository.Fake;
using ShoppingKart.Repository.Interface;
using ShoppingKart.ShoppingBasket.Fake;
using ShoppingKart.ShoppingBasket.Interface;

namespace ShoppingKart
{
	using System.IO;
	using Autofac;
	using ShoppingKart.RuleEngine.Interface;

	internal class Program
	{
		private static void Main(string[] args)
		{
			//IProductCatalogueRepo catalogueRepo = new ProductCatalogueRepo();
			//IInventoryRepo inventory = new InventoryRepo(catalogueRepo);
			//IItemOffersRepo offers = new ItemOffersRepo(catalogueRepo);
			IShoppingBasket basket = new FakeShoppingBasket();
			//IRuleEngine ruleEngine = new RuleEngine.Impl.RuleEngine();

			basket.AddItem('A', 1);
			basket.AddItem('B', 2);
			basket.AddItem('A', 1);
			basket.AddItem('C', 1);
			basket.AddItem('D', 5);
			basket.AddItem('A', 1);
			basket.AddItem('A', 1);
			basket.AddItem('B', 2);


			ContainerBuilder autofac = new ContainerBuilder();
			autofac.Register(
				o =>
					new Cashier.Impl.Cashier(o.Resolve<IProductCatalogueRepo>(), o.Resolve<IInventoryRepo>(),
						o.Resolve<IItemOffersRepo>(), o.Resolve<IRuleEngine>()));
			autofac.Register(o => new InventoryRepo(o.Resolve<IProductCatalogueRepo>()));
			autofac.RegisterType<ProductCatalogueRepo>().As<IProductCatalogueRepo>();
			autofac.RegisterType<InventoryRepo>().As<IInventoryRepo>();
			autofac.RegisterType<ItemOffersRepo>().As<IItemOffersRepo>();
			autofac.RegisterType<RuleEngine.Impl.RuleEngine>().As<IRuleEngine>();
			autofac.RegisterInstance(Console.Out).As<TextWriter>().ExternallyOwned();
			var containerTobuild = autofac.Build();




			using (var scope = containerTobuild.BeginLifetimeScope())
			{

				var inven = scope.Resolve<InventoryRepo>().GetStocks().Where(i => i.QuantityHeld > 0);

				foreach (var inv in inven)
				{
					var firstOrDefault = scope.Resolve<ItemOffersRepo> ( ).GetOffer ( inv.Item.Sku );
					var offerStr = string.Empty;
					if (firstOrDefault != null && firstOrDefault.Offer != null)
					{
						offerStr = firstOrDefault.Offer.ToString();
					}
					Console.WriteLine("{0}\t{1}\t{2}", inv.Item.Sku, inv.Item.FullRetailPrice, offerStr);
				}


				foreach (var inv in inven)
				{
					Console.WriteLine("{0}\t{1}", inv.Item.Sku, inv.Item.FullRetailPrice.ToString("C"));

				}
				var bill = scope.Resolve<Cashier.Impl.Cashier>().Checkout(basket.GetItems());
				foreach (var item in bill)
				{
					Console.WriteLine("{0}\t{1}\t\t{2}", item.Sku, item.Quantity, item.TotalPrice.ToString("C"));
				}
			}

			Console.ReadKey();

		}
	}
}

