using System.Runtime.InteropServices;
using Shops.models;
using Shops.Services;

namespace Shops.Entities;

public class Shop
{
    public Shop(string adress, string name)
    {
        Id = Guid.NewGuid();
        Adress = adress;
        Name = name;
        Products = new List<Product>();
    }

    public List<Product> Products { get; set; }
    public string Adress { get; }
    public string Name { get; set; }
    public Guid Id { get; }

    public Product FindProduct(string name)
    {
        Product empty = new Product("none", 0, 0);
        if (Products.Where(p => p.Name == name).First() != null)
        {
            return Products.Where(p => p.Name == name).First();
        }

        return empty;
    }

    public Product FindProduct(Product name)
    {
        Product empty = new Product("none", 0, 0);
        if (Products.Where(p => p == name).First() != null)
        {
            return Products.Where(p => p == name).First();
        }

        return empty;
    }

    public List<Product> AddProducts(List<Product> products)
    {
        Products.AddRange(products);
        return Products;
    }

    public List<Product> AddProducts(Product product)
    {
        Products.Add(product);
        return Products;
    }

    public Product Sell(BuyWithAmount products)
    {
        if (Products.First(p => p.Name == products.Product.Name) == null || FindProduct(products.Product).Amount < products.Amount)
        {
            throw new AvailabilityException();
        }

        Products.First(p => p.Name == products.Product.Name).Amount -= products.Amount;
        return products.Product;
    }
}