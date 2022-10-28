namespace Shops.Services;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException()
        : base("Customer hasn't enough money.")
    {
    }
}