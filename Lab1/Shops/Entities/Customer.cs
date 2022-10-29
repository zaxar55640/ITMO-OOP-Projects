using Shops.models;
using Shops.Services;

namespace Shops.Entities;

public class Customer
{
    private decimal _balance;
    public Customer(decimal balance, string name)
    {
        if (balance < 0)
        {
            throw new WrongData();
        }

        _balance = balance;
        Name = name;
    }

    public string Name { get; }

    public Customer Buy(decimal price)
    {
        decimal check = _balance - price;
        if (check >= 0)
        {
            _balance -= price;
            return this;
        }
        else
        {
            throw new NotEnoughMoneyException();
        }
    }

    public decimal Balance(Customer customer)
    {
        return _balance;
    }
}