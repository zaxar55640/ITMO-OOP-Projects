using Shops.Entities;
using Shops.Services;
namespace Shops.models;

public class BuyWithAmount
{
    public BuyWithAmount(Product product, int amount)
    {
        if (amount < 0)
        {
            throw new WrongData();
        }

        Product = product;
        Amount = amount;
    }

    public Product Product { get; }
    public int Amount { get; }
}