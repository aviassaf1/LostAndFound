using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;
using Facebook;

namespace Domain.Managers
{
    public class MatchManager : IMatchManager
    {
        private static IMatchManager singleton;

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

        public string changeMatchStatus(int matchID,int statusNum)
        {
            Match match = Cache.getInstance.getMatch(matchID);
            if (match != null)
            {
                if (statusNum == 0)
                    match.MatchStatus = MatchStatus.POSSIBLE;
                if (statusNum == 1)
                {
                    match.MatchStatus = MatchStatus.CORRECT;
                    //remove other matches from that item
                    removeMatchesOfItemExcept(match.MatchID, match.CompanyItemID);
                    if (Cache.getInstance.getCompanyItem(match.Item2ID)!=null)
                        removeMatchesOfItemExcept(match.MatchID, match.Item2ID);
                    //set item state
                }
                if (statusNum == 2)
                    match.MatchStatus = MatchStatus.COMPLETE;
                if (statusNum == 4)
                {
                    match.MatchStatus = MatchStatus.INCORRECT;
                    //delete match?
                }
                else
                    return "not good matchNumber";
                return "status Changed";
            }
            return "no match with that id";
        }

        private void removeMatchesOfItemExcept(int matchID, int companyItemID)
        {
            CompanyItem cItem = Cache.getInstance.getCompanyItem(companyItemID);
            Company company = Cache.getInstance.getCompany(cItem.CompanyName);
            List<Match> matchToRemove = new List<Match>();
            foreach(Match m in company.getComapanyMatches())
            {
                if(matchID!=m.MatchID&&(m.Item2ID==companyItemID|| m.CompanyItemID == companyItemID))
                {
                    matchToRemove.Add(m);
                }
            }
            foreach(Match match in matchToRemove)
            {
                company.removeMatch(match.MatchID);// maybe to remove the fbitem in matches also?
            }
        }

        public List<Match> findMatches(CompanyItem cItem, String token)
        {
            List<Match> newMatches = new List<Match>();
            List<Item> items = new List<Item>();
            List<Item> cItems;
            if (cItem.GetType()==typeof(FoundItem))
                cItems = ComapanyManager.getInstance.getLostItems3Days(cItem.CompanyName, cItem.Date);
            else
                cItems = ComapanyManager.getInstance.getFoundItems3Days(cItem.CompanyName, cItem.Date);
            foreach (Item item in cItems)
            {
                items.Add(item);
            }
            List<Item> FBItems = getFBItemsOfCompany(cItem.CompanyName, token); 
            foreach(Item item in FBItems)
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
                            commentToPost(token, ((FBItem)item).PostID, "שלום, נמצאה התאמה בין הפריט לבין פריט ב" + cItem.CompanyName + " מספר ההתאמה של הפריט הוא: " + match.MatchID);
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
            Company company= Cache.getInstance.getCompany(companyName);
            foreach(String fbGroup in company.FacebookGroups)
            {
                groupFBItems = getPostsFromGroup(token, fbGroup);
                foreach(FBItem fbitem in groupFBItems)
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
                    if (item.Colors.Contains(color))
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

        private Boolean commentToPost(String token,String postID ,String info)
        {
            var fb = new FacebookClient(token);
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            dynamic result = fb.Post(postID + "/comments", new { message = info });
            return true;
        }
        public List<FBItem> getPostsFromGroup(String token, String GroupID)
        {
            List<FBItem> answer = new List<FBItem>();
            var fb = new FacebookClient(token);
            fb.Version = "v2.3";
            var parameters = new Dictionary<string, object>();
            int daysAgo = 3;
            DateTime nDaysAgo = DateTime.Now;
            nDaysAgo = nDaysAgo.AddDays(-daysAgo);
            Int32 unixTimestamp = (Int32)(nDaysAgo.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            parameters["since"] = unixTimestamp;
            dynamic result = fb.Get(GroupID+"/feed"/*, new { since = unixTimestamp }*/);
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
            return answer;
        }

        private FBType getFBType(string description)
        {
            Dictionary<string, FBType> HebTypes = new Dictionary<string, FBType>(){{ "אבד" , FBType.LOST },{ "איבד" , FBType.LOST },{ "איבוד" , FBType.LOST }, { "נעלם", FBType.LOST },
                { "מישהו מצא", FBType.LOST }, { "מישהו במקרה מצא", FBType.LOST }, { "מצאת", FBType.FOUND }, { "מצאנ", FBType.FOUND }, { "נמצא", FBType.FOUND }, { "נימצא", FBType.FOUND }};
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
            Dictionary<string,ItemType> HebTypes = new Dictionary<string, ItemType>(){{ "תעוד" , ItemType.ID },{ "תז" , ItemType.ID },{ "ת\"ז" , ItemType.ID }, { "ארנק", ItemType.WALLET },
                { "עכבר", ItemType.PCMOUSE }, { "מחשב", ItemType.PC }, { "פון", ItemType.PHONE }, { "מפתח", ItemType.KEYS }, { "תיק", ItemType.BAG }, { "מטרי", ItemType.UMBRELLA },
                { "סווטשרט", ItemType.SWEATSHIRT },{ "סווצרט", ItemType.SWEATSHIRT }, { "משקפ", ItemType.GLASSES }, { "נעל", ItemType.SHOES },{ "כפכ", ItemType.FLIPFLOPS },
                { "תיקיה", ItemType.FOLDER },{ "מחברת", ItemType.FOLDER },{ "קלסר", ItemType.FOLDER }, { "מטען", ItemType.CHARGER }, { "עגיל", ItemType.EARING }, { "טבעת", ItemType.RING },
                { "שרשרת", ItemType.NECKLACE },{ "תליון", ItemType.NECKLACE }, { "צמיד", ItemType.BRACELET }, { "אוזני", ItemType.HEADPHONES }};
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
            Dictionary<string,Color> HebColors = new Dictionary<string, Color>(){{ "ורוד" , Color.PINK }, { "שחור", Color.BLACK }, { "כחול", Color.BLUE }, { "אדום", Color.RED }, { "אדומ", Color.RED },
                { "ירוק", Color.GREEN }, { "צהוב", Color.YELLOW }, { "לבן", Color.WHITE }, { "לבנ", Color.WHITE }, { "סגול", Color.PURPEL }, { "כתום", Color.ORANGE }, { "כתומ", Color.ORANGE },
                { "אפור", Color.GRAY }, { "חום", Color.BROWN }, { "חומ", Color.BROWN } , { "זהב", Color.GOLD }, { "זהוב", Color.GOLD }, { "כסף", Color.SILVER }, { "כסוף", Color.SILVER } };
            foreach(string hebCol in HebColors.Keys)
            {
                if (description.Contains(hebCol))
                {
                    colors.Add(HebColors[hebCol]);
                }
            }
            return colors;
        }
    }
}
