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
    public void deleteProfile(string profileToDelete)
    {
        dataService.DeleteStudent(profileToDelete);
    }
}
