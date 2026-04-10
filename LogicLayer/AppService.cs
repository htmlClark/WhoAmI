using DataLayer;

namespace LogicLayer;

public class AppService
{   
    Database db = new Database();

    public void dbInit()
    {
        db.Init();
    }
    public bool isNameValid(string NameFromUser)
    {
        DataService dataService = new DataService();

        if (!string.IsNullOrEmpty(NameFromUser))
        {
            return dataService.doesNameExist(NameFromUser);
        }
        return false;
    }
}
