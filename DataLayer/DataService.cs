using Core;

namespace DataLayer;

public class DataService
{
    List<Student> studentNames = new List<Student>
    {
        new Student
        {
            Name = "clark"
        },
        new Student
        {
            Name = "lily"
        }
    };
    
    public bool doesNameExist(string validName)
    {
        foreach (Student s in studentNames)
        {
            if(s.Name == validName)
            {
                return true;
            }
        }
        return false;
    }
}
