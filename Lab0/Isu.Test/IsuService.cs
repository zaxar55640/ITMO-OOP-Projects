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
        Group group1 = new Group("M32101");
        Student student = new Student("Задорнов Михаил Евгеньевич", group1);
        group1.AddStudent(student);
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

        var exception = Assert.Throws<IsuException>(() => isu.AddStudent(group, "Maxim"));
        Assert.Equal("The group is full.", exception.Message);
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Services.IsuService isu = new Services.IsuService();
        var exception = Assert.Throws<IsuException>(() => isu.AddGroup("1912"));
        Assert.Equal("Wrong group name.", exception.Message);
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Services.IsuService isu = new Services.IsuService();
        Group group1 = isu.AddGroup("M32101");
        Group group2 = isu.AddGroup("M32102");
        Student student = isu.AddStudent(group1, "Maxim");
        isu.ChangeStudentGroup(student, group2);
        Assert.Contains(student, group2.Students);
    }
}