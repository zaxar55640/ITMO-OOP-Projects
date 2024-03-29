using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(string name);
    Student AddStudent(Group group, string name);

    Student? FindStudent(int id);
    List<Student>? FindStudents(GroupName groupName);
    List<Student>? FindStudents(CourseNumber courseNumber);

    Group? FindGroup(GroupName groupName);
    List<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}