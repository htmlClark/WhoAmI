using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using Core;
using DataLayer;

namespace LogicLayer;

public class AppService
{   
    private Database db = new Database();
    private DataService dataService = new DataService();
    public bool isSmaller(string newProfile)
    {
        int stringSize = newProfile.Length;
        
        return stringSize < 3;
    }
    public bool isLarger(string newProfile)
    {
        int stringSize = newProfile.Length;
        
        return stringSize > 16;
    }
    public void addProfile(string validNewProfile)
    {
        if (!isLarger(validNewProfile) && !isSmaller(validNewProfile)) dataService.AddProfile(validNewProfile);
    }
    public bool startDb()
    {
        return db.InitDb();
    }
    public bool profileToLook(string nameFromUser)
    {
        if (!string.IsNullOrWhiteSpace(nameFromUser))
        {
            return dataService.DoesStudentExist(nameFromUser);
        }
        return false;
    }

    public void updateProfile(string oldProfile, string newProfile)
    {
        dataService.UpdateStudent(oldProfile, newProfile);
    }
    public List<Student> FetchProfileFromDb()
    {
        return dataService.GetAllProfiles();
    }
    public void DeleteProfileInDb(string profileToDelete)
    {
        dataService.DeleteStudent(profileToDelete);
    }
    public bool isSpecialCharacters(string profileFromUser)
    {
        foreach(char c in profileFromUser)
        {
            if (!char.IsLetter(c))
            {
                return true;
            }
        }
        return false;
    }
    public bool findDuplicate(string profileToScan)
    {
        List<Student> students = dataService.GetAllProfiles();

        foreach(var s in students)
        {
            if(s.Name == profileToScan)
            {
                return true;
            }
        }
        return false;
    }
}
