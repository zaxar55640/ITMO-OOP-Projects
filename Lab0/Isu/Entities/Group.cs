using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Group
{
    private static int studentAmount = 20;
    public Group(string name)
    {
        Name = new GroupName(name);
        Students = new List<Student>();
    }

    public List<Student> Students { get; set; } = new List<Student>();
    public GroupName Name { get; }

    public Group AddStudent(Student name)
    {
        if (this.Students.Count <= studentAmount)
            Students.Add(name);
        else throw new IsuException("The group is full.");
        return this;
    }

    public Student? FindStudent(int id)
    {
        var person = Students.Where(p => id == p.Id);
        return person.FirstOrDefault();
    }
}