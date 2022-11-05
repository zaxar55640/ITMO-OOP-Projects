namespace Isu.Extra.Exceptions;

public class WrongDataGivenException : Exception
{
    public WrongDataGivenException(string error)
        : base($"Invalid data given into service in :{error}")
    {
    }
}