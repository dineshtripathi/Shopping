using System;

namespace ShoppingKart.Poco
{
    public class MultibuyOffer : Offer
    {
        public long PurchaseQtyForOffer { get; set; }
        public Decimal DiscountedPrice { get; set; }
        public override string ToString()
        {
            return string.Format("{0} for {1}",PurchaseQtyForOffer,DiscountedPrice.ToString("C"));
        }

        public override decimal GetDiscountedAmount(long actualQuantityPurchased,out long unqualifiedQty)
        {
            unqualifiedQty = 0;
            if(actualQuantityPurchased <= 0)
                return 0.0m;

            if (PurchaseQtyForOffer > 0)
            {
                long offerReceivedFactor = actualQuantityPurchased/PurchaseQtyForOffer;
                unqualifiedQty = actualQuantityPurchased % PurchaseQtyForOffer;
                if (offerReceivedFactor > 0)
                {
                    return (offerReceivedFactor*DiscountedPrice);
                }
                return 0.0m;
            }
            return 0.0m;
        }
    }

}