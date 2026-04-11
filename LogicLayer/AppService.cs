using System.Net.Http.Headers;
using DataLayer;

namespace LogicLayer;

public class AppService
{   
    private Database db = new Database();
    private DataService dataService = new DataService();

    public bool dbInit()
    {
        return db.TestConnection();
    }
    public bool isNameValid(string NameFromUser)
    {
        if (!string.IsNullOrWhiteSpace(NameFromUser))
        {
            return dataService.doesNameExist(NameFromUser);
        }
        return false;
    }
}
