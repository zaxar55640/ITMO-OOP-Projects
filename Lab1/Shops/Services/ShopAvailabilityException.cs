namespace Shops.Services;

public class ShopAvailabilityException : Exception
{
    public ShopAvailabilityException()
        : base("Product is not Available.")
    {
    }
}
