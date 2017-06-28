using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;
using Facebook;

namespace WorkerHost.Domain.Managers
{
    public class MatchManager : IMatchManager
    {
        private static IMatchManager singleton;
        private Logger logger = Logger.getInstance;
        public static IMatchManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new MatchManager();
                }
                return singleton;
            }
        }
        public string changeMatchStatus(int matchID, int statusNum, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "changeMatchStatus: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            string companyName = Cache.getInstance.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "changeMatchStatus: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            Company company = CompanyManager.getInstance.getCompanyByName(companyName);
            if (company == null)
            {
                logg = "changeMatchStatus: company name is not valid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            if (!company.Matches.Contains(matchID))
            {
                logg = "changeMatchStatus: match id is not valid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return logg;
            }
            Match match = Cache.getInstance.getMatch(matchID);
            if (match != null)
            {
                if (statusNum == 0)
                    match.MatchStatus = MatchStatus.POSSIBLE;
                else if (statusNum == 1)
                {
                    match.MatchStatus = MatchStatus.CORRECT;
                    removeMatchesOfItemExcept(match.MatchID, match.CompanyItemID);
                    if (Cache.getInstance.getCompanyItem(match.CompanyItemID) != null)
                        removeMatchesOfItemExcept(match.MatchID, match.CompanyItemID);
                    if (Cache.getInstance.getCompanyItem(match.Item2ID) != null)
                        removeMatchesOfItemExcept(match.MatchID, match.Item2ID);
                }
                else if (statusNum == 2)
                {
                    match.MatchStatus = MatchStatus.COMPLETE;
                    CompanyItem item = Cache.getInstance.getCompanyItem(match.CompanyItemID);
                    if (item.GetType() == typeof(FoundItem))
                    {
                        ((FoundItem)item).Delivered = true;
                    }
                    else
                    {
                        ((LostItem)item).WasFound = true;
                    }
                    removeMatchesOfItemExcept(match.MatchID, match.CompanyItemID);
                    CompanyItem item2 = Cache.getInstance.getCompanyItem(match.Item2ID);
                    if (item2 != null)
                    {
                        if (item2.GetType() == typeof(FoundItem))
                        {
                            ((FoundItem)item2).Delivered = true;
                        }
                        else
                        {
                            ((LostItem)item2).WasFound = true;
                        }
                        removeMatchesOfItemExcept(match.MatchID, match.Item2ID);
                    }
                }
                else if (statusNum == 4)
                {
                    match.MatchStatus = MatchStatus.INCORRECT;
                    company.removeMatch(matchID);
                }
                else
                {
                    logg = "not good statusNumber";
                    logger.logPrint(logg, 0);
                    logger.logPrint(logg, 1);
                    return logg;
                }
                logg = "match status Changed";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 1);
                return "status Changed";
            }
            logg = "no match with that id";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return logg;
        }
        private void removeMatchesOfItemExcept(int matchID, int companyItemID)
        {
            CompanyItem cItem = Cache.getInstance.getCompanyItem(companyItemID);
            Company company = Cache.getInstance.getCompany(cItem.CompanyName);
            List<Match> matchToRemove = new List<Match>();
            foreach (Match m in company.getComapanyMatches())
            {
                if (matchID != m.MatchID && (m.Item2ID == companyItemID || m.CompanyItemID == companyItemID))
                {
                    matchToRemove.Add(m);
                }
            }
            foreach (Match match in matchToRemove)
            {
                company.removeMatch(match.MatchID);// maybe to remove the fbitem in matches also?
            }
        }

        public List<Match> findMatches(CompanyItem cItem, String token)
        {
            if (cItem == null || token == null)
                return null;
            List<Match> newMatches = new List<Match>();
            List<Item> items = new List<Item>();
            List<Item> cItems;
            if (cItem.GetType() == typeof(FoundItem))
                cItems = CompanyManager.getInstance.getLostItems3Days(cItem.CompanyName, cItem.Date);
            else
                cItems = CompanyManager.getInstance.getFoundItems3Days(cItem.CompanyName, cItem.Date);
            foreach (Item item in cItems)
            {
                items.Add(item);
            }
            List<Item> FBItems = getFBItemsOfCompany(cItem.CompanyName, token);
            foreach (Item item in FBItems)
            {
                if (Math.Abs(item.Date.Subtract(cItem.Date).Days) < 4){
                    items.Add(item);
                }
            }
            List<Match> matches = Cache.getInstance.getCompany(cItem.CompanyName).getComapanyMatches();
            foreach (Item item in items)
            {
                Boolean addMatch = true;
                foreach (Match m in matches)
                {
                    if ((m.CompanyItemID == cItem.ItemID && m.Item2ID == item.ItemID) || (m.CompanyItemID == item.ItemID && m.Item2ID == cItem.ItemID))
                    {
                        addMatch = false;
                        break;
                    }
                }
                if (addMatch)
                {
                    Match match = findMatch(cItem, item);
                    if (match != null)
                    {
                        newMatches.Add(match);
                        if (item.GetType().Equals(typeof(FBItem)))
                        {
                            item.addToDB();
                            match.Item2ID = item.ItemID;
                            match.addToDB();
                            FacebookConnector.commentToPost(token, ((FBItem)item).PostID, "שלום, נמצאה התאמה בין הפריט לבין פריט ב" + cItem.CompanyName + " מספר ההתאמה של הפריט הוא: " + match.MatchID);
                        }
                        else
                        {
                            match.addToDB();
                        }
                    }
                }
            }
            return newMatches;
        }

        private List<Item> getFBItemsOfCompany(string companyName, string token)
        {
            Company company = Cache.getInstance.getCompany(companyName);
            return FacebookConnector.getFBItemsOfCompany(companyName, token,company.FacebookGroups);
        }

        private Match findMatch(CompanyItem cItem, Item item)
        {
            //use nlp 
            //use image processing
            if ((cItem.GetType() == typeof(LostItem) && (item.GetType() == typeof(FoundItem))) ||
                (cItem.GetType() == typeof(LostItem) && (item.GetType() == typeof(FBItem)) && ((FBItem)item).Type == FBType.FOUND) ||
                (cItem.GetType() == typeof(FoundItem) && (item.GetType() == typeof(LostItem))) ||
                (cItem.GetType() == typeof(FoundItem) && (item.GetType() == typeof(FBItem)) && ((FBItem)item).Type == FBType.LOST))
            {
                Boolean colorMatch = false;
                foreach (Color color in cItem.Colors)
                {
                    if (item.Colors.Contains(color) || item.Colors.Contains(Color.UNKNOWN))
                    {
                        colorMatch = true;
                    }
                }
                if (cItem.ItemType.Equals(item.ItemType) & colorMatch)
                {
                    return new Match(cItem.ItemID, item.ItemID, MatchStatus.POSSIBLE);
                }
            }
            return null;
        }
        
        public Match getMatchByID(int matchID)
        {
            return Cache.getInstance.getMatch(matchID);
        }

        public List<Match> getMatchesByItemID(int itemID, int key)
        {
            string logg;
            String user = SessionDirector.getInstance.getUserName(key);
            if (user == null)
            {
                logg = "getMatchesByItemID: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            string companyName = Cache.getInstance.getCompanyNameByUsername(user);
            if (companyName == null)
            {
                logg = "getMatchesByItemID: session key does not exist";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            Company company = CompanyManager.getInstance.getCompanyByName(companyName);
            if (company == null)
            {
                logg = "getMatchesByItemID: company name is not valid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            if (!(company.LostItems.Contains(itemID)|| company.FoundItems.Contains(itemID)))
            {
                logg = "getMatchesByItemID: item id is not valid";
                logger.logPrint(logg, 0);
                logger.logPrint(logg, 2);
                return null;
            }
            IItemManager iim = ItemManager.getInstance;
            CompanyItem ci = Cache.getInstance.getCompanyItem(itemID);
            if ( ci!= null)
            {
                return Cache.getInstance.getItemMatches(itemID,ci.CompanyName);
            }
            else
                return null;
        }
    }
}
