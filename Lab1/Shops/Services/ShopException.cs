namespace Shops.Services;

public class ShopAlreadyExistsException : Exception
{
    public ShopAlreadyExistsException()
        : base("This shop is already exists.")
    {
    }
}