namespace Isu.Extra.Exceptions;

public class StreamInvalidException : Exception
{
    public StreamInvalidException()
        : base("Invalid stream data given.")
    {
    }
}