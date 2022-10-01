namespace Isu.Services;

public class GroupIsFullException : Exception
    {
        public GroupIsFullException()
            : base("The group is full.")
        {
        }
    }

public class WrongGroupNameException : Exception
    {
        public WrongGroupNameException(string name)
            : base("Wrong group name:" + name)
        {
            Name = name;
        }

        public string Name { get; }
    }

public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException()
        : base("The group is already exists.")
    {
    }
}
