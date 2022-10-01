using System.Security.Cryptography;

namespace Isu.Entities;

public class Student
{
    public Student(string name, Group group)
    {
        Random rnd = new Random();
        Id = rnd.Next(1000000, 9999999);
        Name = name;
        Group = group;
    }

    public string Name { get; }
    public Group Group { get; set; }
    public int Id { get; }
}