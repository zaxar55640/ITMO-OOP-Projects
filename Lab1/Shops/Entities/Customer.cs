using Shops.models;
using Shops.Services;

namespace Shops.Entities;

public class Customer
{
    public Customer(int balance, string name)
    {
        Balance = balance;
        Name = name;
    }

    public float Balance { get; set; }
    public string Name { get; }

    public Customer Buy(BuyWithAmount product)
    {
        float check = Balance - (product.Product.Price * product.Amount);
        if (check >= 0)
        {
            Balance -= product.Product.Price * product.Amount;
            return this;
        }
        else
        {
            throw new NotEnoughMoneyException();
        }
    }
}