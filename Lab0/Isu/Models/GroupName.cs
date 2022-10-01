using Isu.Services;

namespace Isu.Models;

public class GroupName
{
    private readonly string _name;
    private readonly int _groupNumber;

    public GroupName(string name)
    {
        if (name.Length != 6 && Convert.ToInt32(name[1]) > 4)
            throw new WrongGroupNameException(name);
        _name = name;
        CourseNumber = GetCourse(name);
        _groupNumber = GetNumber(name);
    }

    public CourseNumber CourseNumber { get; }

    private static CourseNumber GetCourse(string name)
    {
        int course = Convert.ToInt32(name[1]);
        return (CourseNumber)course;
    }

    private static int GetNumber(string name) => Convert.ToInt32(name[3..]);
}