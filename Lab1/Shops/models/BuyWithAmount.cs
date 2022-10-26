using Shops.Entities;
namespace Shops.models;

public class BuyWithAmount
{
    public BuyWithAmount(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }

    public Product Product { get; }
    public int Amount { get; }
}