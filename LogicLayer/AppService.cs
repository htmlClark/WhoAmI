using DataLayer;

namespace LogicLayer;

public class AppService
{
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
