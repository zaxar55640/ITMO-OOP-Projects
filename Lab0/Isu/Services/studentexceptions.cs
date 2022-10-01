namespace Isu.Services;

public class StudentHasGroupException : Exception
{
    public StudentHasGroupException()
        : base("Student already has a group.")
    {
    }
}