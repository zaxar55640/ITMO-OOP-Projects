namespace Isu.Extra.Exceptions;

public class MaxOgnpPickedException : Exception
{
    public MaxOgnpPickedException()
        : base("Ognp has same faculty as a student.")
    {
    }
}