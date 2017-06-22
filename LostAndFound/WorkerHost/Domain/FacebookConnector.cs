using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using WorkerHost.Domain.BLBackEnd;
using WorkerHost.Domain.Managers;

namespace WorkerHost.Domain
{
    public class FacebookConnector
    {
        public static string testFBToken = "EAACEdEose0cBAHlA9ndfgPFenBoPLJwNSp5Sih71lblqZB7DmLYUTnuRL1ryPHSq3hhJ5ZClPaLvwLMCwjS1N2YbcGmD5eilYqWooNlU6jMaS5szlbAmLMgIShtfhCxZAr1W1YUm6TiF2H02Iv71atk9ZBVMuS74VMaBnjCFpohIPrEClMQpBNPKxM5QgBYZD";
        public static Boolean commentToPost(String token, String postID, String info)
        {
            try {
                var fb = new FacebookClient(token);
                fb.Version = "v2.3";
                var parameters = new Dictionary<string, object>();
                dynamic result = fb.Post(postID + "/comments", new { message = info });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        public static List<FBItem> getPostsFromGroup(String token, String GroupID)
        {
            if (token == null || GroupID == null)
                return null;
            List<FBItem> answer = new List<FBItem>();
            try {
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
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
}
        private static FBType getFBType(string description)
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
        private static List<Color> getColors(string description)
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
        private static ItemType getItemType(string description)
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
        public static List<Item> getFBItemsOfCompany(string companyName, string token, HashSet<String>fbGroups)
        {
            List<FBItem> groupFBItems = new List<FBItem>();
            List<Item> fbItems = new List<Item>();
            foreach (String fbGroup in fbGroups)
            {
                groupFBItems = FacebookConnector.getPostsFromGroup(token, fbGroup);
                foreach (FBItem fbitem in groupFBItems)
                {
                    fbItems.Add(fbitem);
                }
            }
            return fbItems;
        }
        public static String publishInvetory(string token, HashSet<String> fbGroups, int days, List<CompanyItem> items)
        {
            try {
                var fb = new FacebookClient();
                try
                {
                    //make sure the token is good
                    fb = new FacebookClient(token);
                }
                catch
                {
                    return "פרסום נכשל, אנא נסה להתחבר מחדש";
                }
                fb.Version = "v2.3";
                var parameters = new Dictionary<string, object>();
                if (items == null)
                {
                    return "פרסום נכשל, שם חברה לא תקין";
                }
                string inventory = "אלו הפריטים הנמצאים במחלקת אבדות ומציאות: \n";
                string format = " {0} בצבע {1}\n";
                DateTime nDaysAgo = DateTime.Now;
                nDaysAgo = nDaysAgo.AddDays(-days);
                foreach (CompanyItem item in items)
                {
                    if ((item.GetType()).Equals(typeof(FoundItem)))
                    {
                        if (!((FoundItem)item).Delivered && item.Date.CompareTo(nDaysAgo) > 0)
                        {
                            string type = DataType.EnglishTypes2Hebrew[item.ItemType];//DataType.Hebrew2EnglishTypes.FirstOrDefault(x => x.Value == item.ItemType).Key;
                            string color = "";
                            foreach (string col in item.getHebColorsList())
                            {
                                color += col + " ";
                            }
                            inventory += String.Format(format, type, color);
                        }
                    }
                }
                dynamic result = null;
                try
                {
                    //make sure post succeeds with GID
                    //
                    foreach (string groupId in fbGroups)
                    {
                        result = fb.Post(groupId + "/feed", new { message = inventory });
                    }
                    //result = fb.Post("1538105046204967" + "/feed", new { message = inventory });
                }
                catch (Exception ex)
                {
                    return "פרסום נכשל, החיבור עם פייסבוק לא צלח אנא נסה להתחבר שוב לפייסבוק ואז למערכת";
                }
                return "true";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "פרסום נכשל, החיבור עם פייסבוק לא צלח אנא נסה להתחבר שוב לפייסבוק ואז למערכת";
            }
}
        public static Dictionary<string, string> getFBGroups(String token, HashSet<string> FBgroups)
        {
            try {
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
                Dictionary<string, string> result = new Dictionary<string, string>();
                foreach (string groupID in FBgroups)
                {
                    dynamic fbResult = fb.Get(groupID);
                    result.Add(groupID, fbResult["name"]);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
