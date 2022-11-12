using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Service;

public class IsuExtraService
{
    private readonly List<GroupWithFaculty> _listGroups;
    private readonly List<OGNP> _listOgnps;
    private int _studentsId = 0;

    public IsuExtraService()
    {
        _listGroups = new List<GroupWithFaculty>();
        _listOgnps = new List<OGNP>();
    }

    public GroupWithFaculty AddGroup(string name)
    {
        if (name == null)
            throw new WrongDataGivenException("in AddGroup");
        var newGroup = new GroupWithFaculty(name);

        if (_listGroups.Contains(newGroup))
        {
            throw new GroupExistsException();
        }

        _listGroups.Add(newGroup);
        return newGroup;
    }

    public ExtraStudent AddStudent(GroupWithFaculty group, string name)
    {
        if (group == null || name == string.Empty)
            throw new WrongDataGivenException("in AddStudent");
        _studentsId++;
        var currentStudent = new ExtraStudent(name, group, _studentsId);
        group.AddStudent(currentStudent);
        group.ExStudents.Add(currentStudent);
        return currentStudent;
    }

    public OGNP AddOgnp(string facultyName)
    {
        if (string.IsNullOrEmpty(facultyName))
            throw new WrongDataGivenException("in AddOgnp");
        if (_listOgnps.Any(currOgnp => currOgnp.GetFaculty() == facultyName))
        {
            throw new OgnpExistsException();
        }

        var ognp = new OGNP(facultyName);
        _listOgnps.Append(ognp);
        return ognp;
    }

    public void AddStreamToOgnp(OGNP ognp, Stream stream)
    {
        if (ognp == null || stream == null)
            throw new WrongDataGivenException("in AddStreamToOgnp");
        ognp.AddStream(stream);
    }

    public bool CheckSchedule(ExtraStudent student, OGNP ognp, Stream stream)
    {
        if (student == null || ognp == null || stream == null)
            throw new WrongDataGivenException("in CheckSchedule");
        var group = student.GetGroup();
        if (group.GetPairs().Any(p => ognp.FindStream(stream)
                .GetPairs()
                .Any(a => a.BeginLessonTime == p.BeginLessonTime || a.EndLessonTime == p.EndLessonTime)))
        {
            return false;
        }

        return true;
    }

    public void RegisterStudentOnOgnp(ExtraStudent student, OGNP ognp, Stream stream)
    {
        if (student == null || ognp == null || stream == null)
            throw new WrongDataGivenException("in RegisterStudentOnOgnp");
        if (CheckSchedule(student, ognp, stream))
            student.EnrollmentOnOgnp(ognp, stream);
    }

    public void UnregisterStudentOnOgnp(ExtraStudent student, OGNP ognp)
    {
        if (student == null || ognp == null)
            throw new WrongDataGivenException("in Unregiste4rStudentOnOgnp");
        var stream = ognp.GetStreams.First(p => p.GetStudents().Contains(student));
        stream.RemoveStudent(student);
        student.UnEnrollmentOnOgnp(ognp);
    }

    public List<Stream> GetCourseStreams(OGNP ognp)
    {
        if (ognp == null)
            throw new WrongDataGivenException("in GetCourseStreams");
        return ognp.GetStreams.ToList();
    }

    public List<ExtraStudent> GetOGNPStudents(OGNP ognp)
    {
        if (ognp == null)
            throw new WrongDataGivenException("in GetOGNPStudents");
        var list = new List<ExtraStudent>();
        ognp.GetStreams.ToList().ForEach(s => s.GetStudents().ForEach(student => list.Append(student)));
        return list;
    }

    public List<ExtraStudent> StudentsWithoutOgnp(GroupWithFaculty group)
    {
        if (group == null)
            throw new WrongDataGivenException("in StudentsWithoutOgnp");
        return group.ExStudents.Where(p => !p.GetOGNPs().Any()).ToList();
    }
}