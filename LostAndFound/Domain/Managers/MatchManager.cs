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
            return true;
        }
        private List<FBItem> getPostsFromGroup(String token, String GroupID)
        {
            return null;
        }
    }
}
