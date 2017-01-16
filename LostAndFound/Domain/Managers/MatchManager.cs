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

        public List<Match> findMatches(CompanyItem cItem, String token)
        {
            List<Match> newMatches = new List<Match>();
            List<Item> items = new List<Item>();
            List<Item> cItems;
            if (cItem.GetType()==typeof(FoundItem))
                cItems = ComapanyManager.getInstance.getLostItems3Days(cItem.CompanyName, cItem.Date);//get lost or found items of company and add them to items
            else
                cItems = ComapanyManager.getInstance.getFoundItems3Days(cItem.CompanyName, cItem.Date);//get lost or found items of company and add them to items
            foreach (Item item in cItems)
            {
                items.Add(item);
            }
            List<Item> FBItems = getFBItemsOfCompany(cItem.CompanyName, token); //get lost or found item of facebook group and add them to items//make sure that therer are no dup FBItem
            foreach(Item item in FBItems)
            {
                items.Add(item);
            }
            foreach (Item item in items)
            {
                //if havent match in db already!!!
                Match match = findMatch(cItem, item);
                if (match != null)
                    newMatches.Add(match);
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
        private List<FBItem> getPostsFromGroup(String token, String GroupID)
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
            dynamic result = fb.Get(GroupID+"/feed", new { since = unixTimestamp });
            var posts = result["data"];
            foreach (var post in posts)
            {
                DateTime date = 
                FBItem item = new FBItem(List < Color > colors, ItemType itemType, DateTime date, String location, String description,
            String postUrl, String publisherName, FBType fbType);
                Console.WriteLine("post name " + post["from"]["name"]);
                Console.WriteLine("post message " + post["message"]);
            }
            return null;
        }
    }
}
