using System.Security.Cryptography;
using Isu.Extra.Entities;
using Isu.Extra.Service;
using Xunit;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test;

public class ServiceTest
{
    [Fact]
    public void EnrollStudenttoOgnpCourse_StudentGotCourse()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student = isu.AddStudent(group, "VOVA");
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student, ognpcourse, stream);
        Assert.Contains(ognpcourse.FindStream(stream).GetStudents(), p => p == student);
    }

    [Fact]
    public void UnenrollStudentFromOgnp_OgnpGotNoStudentStudentGotRidOfCourse()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student = isu.AddStudent(group, "VOVA");
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student, ognpcourse, stream);
        isu.UnregisterStudentOnOgnp(student, ognpcourse);
        Assert.True(student.GetOGNPs().Count() == 0);
    }

    [Fact]
    public void GetStreamsByCourse()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student = isu.AddStudent(group, "VOVA");
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student, ognpcourse, stream);
        List<Stream> some = isu.GetCourseStreams(ognpcourse);
        Assert.NotEmpty(some);
    }

    [Fact]
    public void GetStudentsFromOgnpCourse_StudentsGiven()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student1 = isu.AddStudent(group, "VOVA");
        ExtraStudent student2 = isu.AddStudent(group, "Vadim");
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student1, ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student2, ognpcourse, stream);
        Assert.Equal(2, ognpcourse.FindStream(stream).GetStudents().Count);
    }

    [Fact]
    public void FindNotSignedToOgnpStudents_StudentsGiven()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student1 = isu.AddStudent(group, "VOVA");
        ExtraStudent student2 = isu.AddStudent(group, "Vadim");
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        isu.RegisterStudentOnOgnp(student1, ognpcourse, stream);
        Assert.Equal(student2, isu.StudentsWithoutOgnp(group).FirstOrDefault());
    }

    [Fact]
    public void CheckScheduleConflicts_ConflictFound()
    {
        IsuExtraService isu = new IsuExtraService();
        GroupWithFaculty group = isu.AddGroup("М32101");
        ExtraStudent student1 = isu.AddStudent(group, "VOVA");
        DateTime date1Begin = new DateTime(2021, 9, 28, 8, 20, 0);
        DateTime date1End = date1Begin.AddHours(1.5);
        DateTime date3Begin = new DateTime(2021, 9, 28, 8, 20, 0);
        DateTime date3End = date3Begin.AddHours(1.5);
        OGNP ognpcourse = isu.AddOgnp("рисование");
        Stream stream = new Stream(1);
        isu.AddStreamToOgnp(ognpcourse, stream);
        var pairforgroup = new Pair(date1Begin, date1End, 3228, "asd", "asdd");
        var pairforognp = new Pair(date3Begin, date3End, 2312, "asdfw", "asd");
        stream.AddLesson(pairforognp);
        group.AddPair(pairforgroup);
        Assert.False(isu.CheckSchedule(student1, ognpcourse, stream));
    }
}