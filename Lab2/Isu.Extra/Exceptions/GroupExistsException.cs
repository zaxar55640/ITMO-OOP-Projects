namespace Isu.Extra.Exceptions;

public class GroupExistsException : Exception
{
    public GroupExistsException()
        : base("Group is already exists.")
    {
    }
}