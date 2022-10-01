using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
   public IsuService()
   {
      Groups = new List<Group>();
   }

   private List<Group> Groups { get; set; }

   public Group AddGroup(string name)
   {
      Group group = new Group(name);
      Groups.Add(group);
      return group;
   }

   public Student AddStudent(Group group, string name)
   {
      Student student = new Student(name, group);
      group.AddStudent(student);
      return student;
   }

   public List<Student>? FindStudents(GroupName groupName)
   {
      var group = Groups.Where(p => p.Name == groupName).FirstOrDefault();
      if (group != null) return group.Students;
      return null;
   }

   public List<Student>? FindStudents(CourseNumber courseNumber)
   {
      var group = Groups.Where(p => p.Name.CourseNumber == courseNumber);
      foreach (var grup in group)
      {
         return grup.Students;
      }

      return null;
   }

   public Group FindGroup(GroupName groupName)
   {
      var group = Groups.Where(p => p.Name == groupName).FirstOrDefault();
      if (group == null)
      {
           throw new IsuException("Wrong name of the group.");
      }

      return group;
   }

   public List<Group> FindGroups(CourseNumber courseNumber)
   {
      var groups = Groups.Where(p => p.Name.CourseNumber == courseNumber);
      return (List<Group>)groups;
   }

   public void ChangeStudentGroup(Student student, Group newGroup)
   {
      Group oldgroup = student.Group;
      oldgroup.Students.Remove(student);
      newGroup.AddStudent(student);
   }

   public Student? FindStudent(int id)
   {
      var group = Groups.Where(p => p.Students.Where(p => p.Id == id) != null);
      Student? student = group.First().Students.Find(p => p.Id == id);
      return student;
   }
}