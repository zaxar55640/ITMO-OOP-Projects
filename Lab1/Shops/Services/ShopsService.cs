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

    public Shop FindShop(Shop shop)
    {
        var sh = ShopList.First(p => p == shop);
        if (sh == null)
            throw new ShopNotFound();
        return sh;
    }

    public List<ProductLot> Shipment(Shop shop, List<ProductLot> products)
    {
        shop.AddProducts(products);
        return (List<ProductLot>)shop.Products;
    }

    public BuyWithAmount ShopSells(Shop shop, Customer customer, BuyWithAmount product)
    {
        ProductLot productlot = shop.Products.First(p => p.PProduct == product.Product);
        var check = shop.Products.Where(p => p.PProduct.Name == product.Product.Name && p.Amount >= product.Amount)
            .Any();
        if (check)
        {
            decimal price = product.Amount * productlot.Price;
            customer.Buy(price);
            shop.Sell(product);
        }
        else
        {
            throw new ShopAvailabilityException();
        }

        return product;
    }

    public decimal SetNewPrice(Shop shop, Product product, decimal newprice)
    {
        return shop.Products.First(p => p.PProduct == product).ChangePrice(newprice);
    }

    public decimal CheckPrice(Shop shop, Product product)
    {
        return shop.FindProduct(product.Name).Price;
    }

    public Shop FindCheapestShop(List<BuyWithAmount> buy)
    {
        var list = ShopList
            .OrderBy(p => p.CheckPrice(buy));
        return list.First();
    }
}