using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingKart.Poco;
using ShoppingKart.Repository.Interface;

namespace ShoppingKart.Repository.Fake
{
    public class ItemOffersRepo : IItemOffersRepo
    {
        private static readonly object SyncObject = new object();

        private static readonly List<ItemOffer> ItemOffers = new List<ItemOffer>();
        private readonly IProductCatalogueRepo _productCatalogueRepo;

        public ItemOffersRepo(IProductCatalogueRepo productCatalogueRepo)
        {
            _productCatalogueRepo = productCatalogueRepo;
            if (ItemOffers.Count == 0)
            {
                ItemOffers.AddRange(new[]
                {
                    new ItemOffer() {Item = productCatalogueRepo.GetItem('A'), Offer = new MultibuyOffer(){OfferId = 1,DiscountedPrice = 13.0m,PurchaseQtyForOffer = 3}},
                    new ItemOffer() {Item = productCatalogueRepo.GetItem('B'), Offer = new MultibuyOffer(){OfferId = 2,DiscountedPrice = 4.5m,PurchaseQtyForOffer = 2}}
                });
            }
        }

        public IQueryable<ItemOffer> GetOffers()
        {
            lock (SyncObject)
            {
                return ItemOffers.AsReadOnly().AsQueryable();
            }
        }

        public ItemOffer GetOffer(char sku)
        {
            lock (SyncObject)
            {
                return ItemOffers.FirstOrDefault(p => p.Item.Sku == sku);
            }
        }

        public IQueryable<ItemOffer> GetOffers(char sku)
        {
            lock (SyncObject)
            {
                return ItemOffers.FindAll(p => p.Item.Sku == sku).AsReadOnly().AsQueryable();
            }
        }

        public bool AddOffer(char sku, Offer offer)
        {
            if (offer == null)
                throw new ArgumentNullException("offer");
            if (offer.OfferId <= 0)
                throw new ArgumentException("Invalid offerId");
            lock (SyncObject)
            {
                var item = _productCatalogueRepo.GetItem(sku);
                if (item != null)
                {
                    if (!ItemOffers.Exists(p => p.Offer.OfferId == offer.OfferId))
                    {
                        var itemOffer = new ItemOffer {Item = item, Offer = offer};
                        ItemOffers.Add(itemOffer);
                    }
                }
            }
            return false;
        }

        public bool RemoveOffer(char sku, long offerId)
        {
            if (offerId <= 0)
                throw new ArgumentException("Invalid offerId");
            lock (SyncObject)
            {
                var item = _productCatalogueRepo.GetItem(sku);
                if (item != null)
                {
                    var toRemove = ItemOffers.FirstOrDefault(p => p.Offer.OfferId == offerId);
                    if (toRemove != null)
                    {
                        ItemOffers.Remove(toRemove);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}