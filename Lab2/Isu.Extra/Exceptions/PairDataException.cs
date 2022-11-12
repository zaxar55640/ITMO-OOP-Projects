namespace Isu.Extra.Exceptions;

public class PairDataException : Exception
{
    public PairDataException()
        : base("Wrong pair data given.")
    {
    }
}