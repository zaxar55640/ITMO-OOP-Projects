using System.Net.Sockets;
using Shops.Entities;
using Shops.models;
using Shops.Services;

using Xunit;

namespace Shops.Test;

public class ShopTests
{
    [Fact]
    public void ShipmentToShop_ProductsAreAvailable()
    {
        ShopsService service = new ShopsService();
        Shop shop = service.AddShop("Gazmyas 22", "8rochka");
        Customer customer = new Customer(10000, "Sosa");
        Product bread = new Product("Bread", 10, 50);
        BuyWithAmount buy = new BuyWithAmount(bread, 10);
        List<Product> products = new List<Product>() { bread, new Product("Eggs", 3, 100) };
        service.Shipment(shop, products);
        var productbefore = shop.Products.First(p => p == bread);
        Assert.True(productbefore.Amount != 0);
        service.ShopSells(shop, customer, buy);
        var productafter = shop.Products.First(p => p == bread);
        Assert.True(productafter.Amount == 0);
    }

    [Fact]
    public void SetPrice_PriceChanged()
    {
        ShopsService service = new ShopsService();
        Shop shop = service.AddShop("Gazmyas 22", "8rochka");
        Product bread = new Product("Bread", 10, 50);
        shop.AddProducts(bread);
        service.SetNewPrice(shop, bread, 25);
        Product newpricedbread = shop.Products.First(p => p.Name == bread.Name);
        Assert.Equal(25, newpricedbread.Price);
    }

    [Fact]
    public void SearchForCheapestOpportunity_ShopFound()
    {
        ShopsService service = new ShopsService();
        Shop shop1 = service.AddShop("Gazmyas 22", "8rochka");
        Shop shop2 = service.AddShop("Nikogdaeva 1", "5rochka");
        Shop shop3 = service.AddShop("Plohova 14", "VkusVill");
        Product bread = new Product("bread", 10, 50);
        Product bread1 = new Product(bread, 100);
        Product bread2 = new Product(bread, 120);
        bread1.Amount = 4;
        bread2.Amount = 100;
        service.FindShop(shop1).AddProducts(bread);
        service.FindShop(shop2).AddProducts(bread1);
        service.FindShop(shop3).AddProducts(bread2);
        BuyWithAmount buy = new BuyWithAmount(bread, 10);
        Assert.Equal(shop1, service.FindCheapestShop(buy, new Customer(10000, "Vanya")));
    }

    [Fact]
    public void BuyWithAmount_MoneyAndAmountAltered()
    {
        ShopsService service = new ShopsService();
        Product bread = new Product("bread", 10, 50);
        BuyWithAmount buy = new BuyWithAmount(bread, 5);
        Customer customer = new Customer(250, "Vanya");
        Shop shop = service.AddShop("Gazmyas 22", "8rochka");
        service.FindShop(shop).AddProducts(bread);
        service.ShopSells(shop, customer, buy);
        Assert.Equal(5, shop.FindProduct(bread).Amount);
        Assert.Equal(0, customer.Balance);
    }
}