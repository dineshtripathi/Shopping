using System;
using ShoppingKart.Poco;
using ShoppingKart.RuleEngine.Interface;

namespace ShoppingKart.RuleEngine.Impl
{
    public class RuleEngine : IRuleEngine
    {

        public decimal GetAmount(ItemOffer itemOffer, long purchasedQuantity)
        {
            if(itemOffer == null)
                throw new ArgumentNullException("itemOffer");
            if (itemOffer.Offer == null)
                throw new ArgumentException("Invalid itemOffer.Offer");
            if (itemOffer.Item == null)
                throw new ArgumentException("Invalid itemOffer.Item");
            decimal totalAmount = 0.0m;
            long remainderQty;
            totalAmount = itemOffer.Offer.GetDiscountedAmount(purchasedQuantity, out remainderQty);
            if (remainderQty > 0)
            {
                totalAmount += remainderQty*itemOffer.Item.FullRetailPrice;
            }

            return totalAmount;
        }

    }
}
