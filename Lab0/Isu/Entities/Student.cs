using System.Security.Cryptography;

namespace Isu.Entities;

public class Student
{
    public Student(string name, Group group, int id)
    {
        Id = id;
        Name = name;
        Group = group;
    }

    public string Name { get; }
    public Group Group { get; set; }
    public int Id { get; }
}