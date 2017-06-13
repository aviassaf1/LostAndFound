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
                if (statusNum == 1)
                {
                    match.MatchStatus = MatchStatus.CORRECT;
                    removeMatchesOfItemExcept(match.MatchID, match.CompanyItemID);
                    if (Cache.getInstance.getCompanyItem(match.Item2ID) != null)
                        removeMatchesOfItemExcept(match.MatchID, match.Item2ID);
                }
                if (statusNum == 2)
                    match.MatchStatus = MatchStatus.COMPLETE;
                if (statusNum == 4)
                {
                    match.MatchStatus = MatchStatus.INCORRECT;
                    match.delete();
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
                items.Add(item);
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
                            commentToPost(token, ((FBItem)item).PostID, "שלום,\n"+"בזכות מערכת אבדות ומציאות שפותחה על ידי סטודנטים מאוניברסיטת בן גוריון,\n"+"נמצאה התאמה בין הפריט לבין פריט ב" + cItem.CompanyName + " מספר ההתאמה של הפריט הוא: " + match.MatchID);
                        }
                    }
                }
            }
            return newMatches;
        }

        private List<Item> getFBItemsOfCompany(string companyName, string token)
        {
            List<Item> fbItems = new List<Item>();
            List<FBItem> groupFBItems = new List<FBItem>();
            Company company = Cache.getInstance.getCompany(companyName);
            foreach (String fbGroup in company.FacebookGroups)
            {
                groupFBItems = getPostsFromGroup(token, fbGroup);
                foreach (FBItem fbitem in groupFBItems)
                {
                    fbItems.Add(fbitem);
                }
            }
            return fbItems;
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

        private Boolean commentToPost(String token, String postID, String info)
        {
            var fb = new FacebookClient(token);
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            dynamic result = fb.Post(postID + "/comments", new { message = info });
            string logg = "post commented";
            logger.logPrint(logg, 0);
            logger.logPrint(logg, 1);
            return true;
        }
        public List<FBItem> getPostsFromGroup(String token, String GroupID)
        {
            if (token == null || GroupID == null)
                return null;
            List<FBItem> answer = new List<FBItem>();
            var fb = new FacebookClient();
            try
            {
                //make sure the token is good
                fb = new FacebookClient(token);
            }
            catch
            {
                return null;
            }
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            int daysAgo = 3;
            DateTime nDaysAgo = DateTime.Now;
            nDaysAgo = nDaysAgo.AddDays(-daysAgo);
            Int32 unixTimestamp = (Int32)(nDaysAgo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            parameters["since"] = unixTimestamp;
            dynamic result;
            try
            {
                //make sure post succeeds with GID
                result = fb.Get(GroupID + "/feed"/*, new { since = unixTimestamp }*/);
            }
            catch (Exception)
            {

                return null;
            }
            var posts = result["data"];
            foreach (var post in posts)
            {
                JsonObject npost = (JsonObject)post;
                string postID = post["id"];
                FBItem fbi = Cache.getInstance.getFBItemByPostID(postID);
                if (fbi != null)
                    answer.Add(fbi);
                else
                {
                    if (npost.ContainsKey("message"))
                    {
                        string description = post["message"];
                        if (!description.Contains("אלו הפריטים הנמצאים"))
                        {
                            FBType fbType = getFBType(description);
                            if (fbType != FBType.NO)
                            {
                                DateTime date = DateTime.Parse(post["created_time"]);
                                string publisher = post["from"]["name"];
                                List<Color> colors = getColors(description);
                                ItemType itemType = getItemType(description);
                                string location = "NeverLand";//"getLocation(description);
                                FBItem item = new FBItem(colors, itemType, date, location, description, postID, publisher, fbType);
                                answer.Add(item);
                            }
                        }
                    }
                }
            }
            return answer;
        }

        private FBType getFBType(string description)
        {
            Dictionary<string, FBType> HebTypes = DataType.HebTypes;
            foreach (string hebType in HebTypes.Keys)
            {
                if (description.Contains(hebType))
                {
                    return HebTypes[hebType];
                }
            }
            return FBType.NO;
        }

        private ItemType getItemType(string description)
        {
            Dictionary<string, ItemType> HebTypes = DataType.Hebrew2EnglishTypes;
            foreach (string hebType in HebTypes.Keys)
            {
                if (description.Contains(hebType))
                {
                    return HebTypes[hebType];
                }
            }
            return ItemType.UNDEFIEND;

        }

        private List<Color> getColors(string description)
        {
            List<Color> colors = new List<Color>();
            Dictionary<string, Color> HebColors = DataType.HebColors;
            Boolean foundColor = false;
            foreach (string hebCol in HebColors.Keys)
            {
                if (description.Contains(hebCol))
                {
                    colors.Add(HebColors[hebCol]);
                    foundColor = true;
                }
            }
            if (foundColor == false)
            {
                colors.Add(Color.UNKNOWN);
            }
            return colors;
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
