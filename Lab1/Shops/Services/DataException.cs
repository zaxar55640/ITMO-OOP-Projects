namespace Shops.Services;
public class WrongData : Exception
{
    public WrongData()
        : base("Given wrong data.")
    {
    }
}