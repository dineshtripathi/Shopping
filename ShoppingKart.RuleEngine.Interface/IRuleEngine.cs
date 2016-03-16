
using ShoppingKart.Poco;

namespace ShoppingKart.RuleEngine.Interface
{
    public interface IRuleEngine
    {
        decimal GetAmount(ItemOffer itemOffer,long purchasedQuantity);
    }
}
