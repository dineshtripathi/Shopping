namespace ShoppingKart.Poco
{
    public abstract class Offer
    {
        public long OfferId { get;  set; }
        public abstract decimal GetDiscountedAmount(long actualQuantityPurchased, out long unqualifiedQty);
    }
}