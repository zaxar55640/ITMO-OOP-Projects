namespace Isu.Extra.Entities;

public class Pair
{
    private readonly int _lectureRoomNumber;
    private readonly string _teacherName;
    private readonly string _lessonName;

    public Pair(DateTime lessonBegin, DateTime lessonEnd, int roomNumber, string teacherName, string lessonName)
    {
        BeginLessonTime = lessonBegin;
        EndLessonTime = lessonEnd;
        _lectureRoomNumber = roomNumber;
        _teacherName = teacherName;
        _lessonName = lessonName;
    }

    public DateTime BeginLessonTime { get; }
    public DateTime EndLessonTime { get; }
}