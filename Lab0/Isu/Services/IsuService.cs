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
      if (!Groups.Contains(group))
      {
         Groups.Add(group);
      }
      else
      {
         throw new GroupAlreadyExistsException();
      }

      return group;
   }

   public Student AddStudent(Group group, string name)
   {
      if (string.IsNullOrEmpty(name) || group == null)
      {
         throw new WrongData();
      }

      Random rnd = new Random();
      int id = rnd.Next(1000000, 9999999);
      Student student = new Student(name, group, id);
      if (!group.Students.Contains(student))
      {
         group.AddStudent(student);
      }
      else
      {
         throw new StudentHasGroupException();
      }

      return student;
   }

   public List<Student> FindStudents(GroupName groupName)
   {
      List<Student> emptylist = new List<Student>();
      var group = Groups.Where(p => p.Name == groupName).FirstOrDefault();
      if (group != null) return group.Students;
      return emptylist;
   }

   public List<Student> FindStudents(CourseNumber courseNumber)
   {
      List<Student> emptylist = new List<Student>();
      var group = Groups.Where(p => p.Name.CourseNumber == courseNumber);
      foreach (var grup in group)
      {
         return grup.Students;
      }

      return emptylist;
   }

   public Group FindGroup(GroupName groupName)
   {
      var group = Groups.Where(p => p.Name == groupName).FirstOrDefault();
      if (group == null)
      {
           throw new WrongGroupNameException();
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
      if (newGroup.AvailabilityofGroup())
      {
         oldgroup.Students.Remove(student);
         newGroup.AddStudent(student);
      }
      else
      {
         throw new GroupIsFullException();
      }
   }

   public Student? FindStudent(int id)
   {
      var group = Groups.Where(p => p.Students.Where(p => p.Id == id) != null);
      Student? student = group.First().Students.Find(p => p.Id == id);
      return student;
   }
}