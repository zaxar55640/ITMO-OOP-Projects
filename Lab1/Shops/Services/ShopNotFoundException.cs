namespace Shops.Services;

public class ShopNotFound : Exception
{
    public ShopNotFound()
        : base("Shop not found.")
    {
    }
}