namespace Isu.Services;

public class Dataexception : Exception
{
    public Dataexception()
        : base("Wrong data were given.")
    {
    }
}