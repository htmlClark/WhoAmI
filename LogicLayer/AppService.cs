using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
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
    public bool startDb()
    {
        return db.InitDb();
    }
    public bool isNameValid(string nameFromUser)
    {
        if (!string.IsNullOrWhiteSpace(nameFromUser))
        {
            return dataService.DoesStudentExist(nameFromUser);
        }
        return false;
    }
}
