using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class Stream
{
    private readonly int _streamNumber;
    private readonly List<ExtraStudent> _studentsList;
    private readonly List<Pair> _pairs;

    public Stream(int streamNumber)
    {
        _streamNumber = streamNumber;
        _pairs = new List<Pair>();
        _studentsList = new List<ExtraStudent>();
    }

    public void AddStudent(ExtraStudent student)
    {
        if (student is null)
        {
            throw new StreamInvalidException();
        }

        _studentsList.Add(student);
    }

    public void RemoveStudent(ExtraStudent student)
    {
        if (student is null)
        {
            throw new StreamInvalidException();
        }

        _studentsList.Remove(student);
    }

    public List<ExtraStudent> GetStudents() => _studentsList;
    public List<Pair> GetPairs() => _pairs;
    public void AddLesson(Pair pair) => _pairs.Add(pair);
}