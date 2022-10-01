using System.Threading.Tasks.Dataflow;
using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuService
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Services.IsuService isu = new Services.IsuService();
        Group group1 = isu.AddGroup("M32222");
        Student student = isu.AddStudent(group1, "Задорнов Михаил Евгеньевич");
        Assert.Contains(student, group1.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Services.IsuService isu = new Services.IsuService();
        Group group = isu.AddGroup("M32101");
        for (int i = 0; i <= 20; i++)
        {
            string name = Convert.ToString(i);
            isu.AddStudent(group, name);
        }

        Assert.Throws<GroupIsFullException>(() => isu.AddStudent(group, "Maxim"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Services.IsuService isu = new Services.IsuService();
        Assert.Throws<WrongGroupNameException>(() => isu.AddGroup("1912"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Services.IsuService isu = new Services.IsuService();
        Group group1 = isu.AddGroup("M32155");
        Group group2 = isu.AddGroup("M32123");
        Student student = isu.AddStudent(group1, "Maxim");
        isu.ChangeStudentGroup(student, group2);
        Assert.Contains(student, group2.Students);
    }
}