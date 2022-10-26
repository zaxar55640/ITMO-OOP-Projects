namespace Shops.Entities;

public class Product
{
    public Product(string name, int amount, float price)
    {
        Name = name;
        Amount = amount;
        Price = price;
    }

    public Product(Product product, float newPrice)
    {
        Name = product.Name;
        Amount = product.Amount;
        Price = newPrice;
    }

    public string Name { get; set; }
    public int Amount { get; set; }
    public float Price { get; set; }
}