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
        Product bread = new Product("Bread");
        ProductLot breadlot = new ProductLot(bread, 10, 50);
        BuyWithAmount buy = new BuyWithAmount(bread, 10);
        List<ProductLot> products = new List<ProductLot>() { breadlot, new ProductLot(new Product("Eggs"), 3, 100) };
        service.Shipment(shop, products);
        var productbefore = shop.Products.First(p => p.PProduct == bread);
        Assert.True(productbefore.Amount != 0);
        service.ShopSells(shop, customer, buy);
        var productafter = shop.Products.First(p => p.PProduct == bread);
        Assert.True(productafter.Amount == 0);
    }

    [Fact]
    public void SetPrice_PriceChanged()
    {
        ShopsService service = new ShopsService();
        Shop shop = service.AddShop("Gazmyas 22", "8rochka");
        Product bread = new Product("Bread");
        ProductLot productLot = new ProductLot(bread, 10, 50);
        shop.AddProducts(productLot);
        service.SetNewPrice(shop, bread, 25);
        ProductLot newpricedbread = shop.Products.First(p => p.PProduct == bread);
        Assert.Equal(25, newpricedbread.Price);
    }

    [Fact]
    public void SearchForCheapestOpportunity_ShopFound()
    {
        ShopsService service = new ShopsService();
        Shop shop1 = service.AddShop("Gazmyas 22", "8rochka");
        Shop shop2 = service.AddShop("Nikogdaeva 1", "5rochka");
        Shop shop3 = service.AddShop("Nikogdaeva 12", "123rochka");
        Product bread = new Product("bread");
        Product eggs = new Product("eggs");
        List<ProductLot> forshop1 = new List<ProductLot>() { new ProductLot(bread, 10, 100), new ProductLot(eggs, 3, 100) };
        List<ProductLot> forshop2 = new List<ProductLot>() { new ProductLot(bread, 10, 50), new ProductLot(eggs, 3, 90) };
        List<ProductLot> forshop3 = new List<ProductLot>() { new ProductLot(bread, 5, 50), new ProductLot(eggs, 3, 100) };
        service.FindShop(shop1).AddProducts(forshop1);
        service.FindShop(shop2).AddProducts(forshop2);
        service.FindShop(shop3).AddProducts(forshop3);
        BuyWithAmount buy1 = new BuyWithAmount(bread, 10);
        BuyWithAmount buy2 = new BuyWithAmount(eggs, 3);
        List<BuyWithAmount> buy = new List<BuyWithAmount>() { buy1, buy2 };
        Assert.Equal(shop2, service.FindCheapestShop(buy));
    }

    [Fact]
    public void BuyWithAmount_MoneyAndAmountAltered()
    {
        ShopsService service = new ShopsService();
        Product bread = new Product("bread");
        ProductLot breadlot = new ProductLot(bread, 10, 50);
        BuyWithAmount buy = new BuyWithAmount(bread, 5);
        Customer customer = new Customer(250, "Vanya");
        Shop shop = service.AddShop("Gazmyas 22", "8rochka");
        service.FindShop(shop).AddProducts(breadlot);
        service.ShopSells(shop, customer, buy);
        Assert.Equal(5, shop.FindProduct(bread.Name).Amount);
        Assert.Equal(0, customer.Balance(customer));
    }
}