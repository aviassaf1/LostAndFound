using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;

namespace WorkerHost.Domain.Managers
{
    public interface ICompanyManager
    {
        String login(String token, String userName, String userPassword);
        Company getCompanyByName(string companyName); //returns null if fails
        String publishInventory( String GroupID, int days, int key);//max days=8, string "true" is ok
        List<Item> getLostItems3Days(string companyName, DateTime date); //returns null if fails
        List<Item> getFoundItems3Days(string companyName, DateTime date); //returns null if fails
        String addFBGroup( string groupID, int key); //return true if good
        String removeFBGroup( string groupID, int key); //return true if good
        Dictionary<string, string> getSystemCompanyFBGroup( int key); //return null if fails
        String removeWorker(String delUsername, int key);
        String addWorker(String newUsername, String newPassword, bool isManager, int key);
        string getToken(string companyName);
        Dictionary<string, bool> getCompanyWorkers(int key);
        bool isManager(int key);
        void setToken(String companyName, string token);
    }
}
