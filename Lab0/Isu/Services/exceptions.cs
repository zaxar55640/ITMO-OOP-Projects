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
        public WrongGroupNameException()
            : base("Wrong group name.")
        {
        }
    }

public class StudentHasGroupException : Exception
    {
        public StudentHasGroupException()
            : base("Student already has a group.")
        {
        }
    }

public class GroupAlreadyExistsException : Exception
{
    public GroupAlreadyExistsException()
        : base("The group is already exists.")
    {
    }
}

public class WrongData : Exception
{
    public WrongData()
        : base("Wrong data were given.")
    {
    }
}