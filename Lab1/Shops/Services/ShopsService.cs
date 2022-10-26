using System.Data.SqlTypes;
using Shops.Entities;
using Shops.models;
using Shops.Services;

namespace Shops.Services;

public class ShopsService
{
    public ShopsService()
    {
        ShopList = new List<Shop>();
    }

    private List<Shop> ShopList { get; set; }

    public Shop AddShop(string adress, string name)
    {
        var check = ShopList.Where(p => p.Name == name && p.Adress == adress);
        if (!check.Any())
        {
            Shop shop = new Shop(adress, name);
            ShopList.Add(shop);
            return shop;
        }
        else
        {
            throw new ShopAlreadyExistsException();
        }
    }

    public Product? FindProduct(Product product)
    {
        return ShopList.First(p => p.Products.Contains(product)).FindProduct(product);
    }

    public Shop FindShop(Shop shop)
    {
        if (ShopList.First(p => p == shop) == null)
            throw new ShopNotFound();
        return ShopList.First(p => p == shop);
    }

    public List<Product> Shipment(Shop shop, List<Product> products)
    {
        shop.AddProducts(products);
        return (List<Product>)shop.Products;
    }

    public BuyWithAmount ShopSells(Shop shop, Customer customer, BuyWithAmount product)
    {
        if (shop.Products.Where(p => p.Name == product.Product.Name && p.Amount >= product.Amount).Any())
        {
            customer.Buy(product);
            shop.Sell(product);
        }
        else
        {
            throw new AvailabilityException();
        }

        return product;
    }

    public Product SetNewPrice(Shop shop, Product product, float newprice)
    {
        shop.Products.Remove(product);
        Product newpricedproduct = new Product(product, newprice);
        shop.AddProducts(newpricedproduct);
        return newpricedproduct;
    }

    public float CheckPrice(Shop shop, Product product)
    {
        return shop.FindProduct(product).Price;
    }

    public Shop FindCheapestShop(BuyWithAmount buy, Customer customer)
    {
        var list = ShopList
            .Where(shop => shop.FindProduct(buy.Product.Name).Name != "none" && shop.FindProduct(buy.Product.Name).Amount >= buy.Amount)
            .OrderBy(shop => shop.FindProduct(buy.Product.Name).Price);
        return list.First();
    }
}