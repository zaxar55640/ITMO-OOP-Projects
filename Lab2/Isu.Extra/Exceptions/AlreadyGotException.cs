namespace Isu.Extra.Exceptions;

public class StudentAlreadyEnrolled : Exception
{
    public StudentAlreadyEnrolled()
        : base("Student already got this course.")
    {
    }
}