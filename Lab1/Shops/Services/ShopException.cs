namespace Shops.Services;

public class ShopExistsException : Exception
{
    public ShopExistsException()
        : base("This shop is already exists.")
    {
    }
}