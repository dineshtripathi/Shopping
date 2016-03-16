using System.Collections.Generic;

namespace ShoppingKart.Cashier.Interface
{
    public interface ICashier
    {
        IEnumerable<BillItem> Checkout(IEnumerable<char> skuEnumerable);

    }
}
