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
        public string changeMatchStatus()
        {
            throw new NotImplementedException();
        }

        public List<Match> findMatches(CompanyItem item, String token)
        {
            throw new NotImplementedException();
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
            return null;
        }
    }
}
