namespace Isu.Extra.Exceptions;

public class ScheduleconflictException : Exception
{
    public ScheduleconflictException()
        : base("Schedules conflict.")
    {
    }
}