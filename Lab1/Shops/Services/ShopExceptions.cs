namespace Shops.Services;

public class ShopExceptions : Exception
{
    public ShopExceptions()
        : base("123")
    {
    }
}

public class AvailabilityException : Exception
{
    public AvailabilityException()
        : base("Product is not Available.")
    {
    }
}

public class ShopAlreadyExistsException : Exception
{
    public ShopAlreadyExistsException()
        : base("This shop is already exists.")
    {
    }
}

public class ShopNotFound : Exception
{
    public ShopNotFound()
        : base("Shop not found.")
    {
    }
}