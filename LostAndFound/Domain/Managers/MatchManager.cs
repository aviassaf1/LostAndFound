using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public class MatchManager : IMatchManager
    {
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
            //get lost or found items of company and add them to items
            //get lost or found item of facebook group and add them to items
            foreach(Item item in items)
            {
                //if havent match in db already!!!
                Match match = findMatch(cItem, item);
                if (match != null)
                    newMatches.Add(match);
            }
            return newMatches;
        }

        private Match findMatch(CompanyItem cItem, Item item)
        {
            //use nlp 
            //use image processing
            Boolean colorMatch = false;
            foreach(Color color in cItem.Colors)
            {
                if (item.Colors.Contains(color))
                {
                    colorMatch = true;
                }
            }
            if (cItem.ItemType.Equals(item.ItemType)&colorMatch)
            {
                return new Match(cItem.ItemID, item.ItemID, MatchStatus.POSSIBLE);
            }
            return null;
        }

        private Boolean commentToPost(String token,String postID ,String info)
        {
            return true;
        }
        private List<FBItem> getPostsFromGroup(String token, String GroupID, Boolean isLost)
        {
            return null;
        }
    }
}
