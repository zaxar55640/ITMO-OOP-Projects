namespace Isu.Extra.Exceptions;

public class OgnpExistsException : Exception
{
    public OgnpExistsException()
        : base("Ognp is already exists.")
    {
    }
}