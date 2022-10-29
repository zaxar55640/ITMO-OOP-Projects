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
        Products = new List<ProductLot>();
    }

    public List<ProductLot> Products { get; set; }
    public string Adress { get; }
    public string Name { get; set; }
    public Guid Id { get; }

    public ProductLot FindProduct(string name)
    {
        ProductLot empty = new ProductLot(new Product("none"), 0, 0);
        var product = Products.FirstOrDefault(p => p.PProduct.Name == name);
        if (product != null)
        {
            return product;
        }

        return empty;
    }

    public List<ProductLot> AddProducts(List<ProductLot> products)
    {
        products.ForEach(p => AddProducts(p));
        return Products;
    }

    public List<ProductLot> AddProducts(ProductLot product)
    {
        if (FindProduct(product.PProduct.Name).PProduct.Name == "none")
        {
            Products.Add(product);
        }
        else
        {
            FindProduct(product.PProduct.Name).Amount += product.Amount;
        }

        return Products;
    }

    public Product Sell(BuyWithAmount products)
    {
        if (FindProduct(products.Product.Name).PProduct.Name == "none" || FindProduct(products.Product.Name).Amount < products.Amount)
        {
            throw new ShopAvailabilityException();
        }

        Products.First(p => p.PProduct.Name == products.Product.Name).Amount -= products.Amount;
        return products.Product;
    }

    public decimal CheckPrice(List<BuyWithAmount> buy)
    {
        var list = Products.Where(p => p.PProduct == buy.FirstOrDefault(b => b.Product == p.PProduct && b.Amount <= p.Amount)?.Product);
        if (list.Count() == buy.Count())
        {
            return list.Sum(p => buy.First(b => b.Product == p.PProduct).Amount * p.Price);
        }

        return 9999999999999;
    }
}