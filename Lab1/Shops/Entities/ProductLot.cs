using Shops.Services;
namespace Shops.Entities;

public class ProductLot
{
    public ProductLot(Product product, int amount, decimal newPrice)
    {
        if (amount < 0 || newPrice < 0)
        {
            throw new WrongData();
        }

        PProduct = product;
        Amount = amount;
        Price = newPrice;
    }

    public Product PProduct { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }

    public decimal ChangePrice(decimal newPrice)
    {
        Price = newPrice;
        return newPrice;
    }
}