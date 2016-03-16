using System.Linq;
using ShoppingKart.Poco;

namespace ShoppingKart.Repository.Interface
{
    public interface IItemOffersRepo
    {
        IQueryable<ItemOffer> GetOffers();
        IQueryable<ItemOffer> GetOffers(char sku);
        ItemOffer GetOffer(char sku);
        bool AddOffer(char sku,Offer offer);
        bool RemoveOffer(char sku, long offerId);

    }
}
