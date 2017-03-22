using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.BLBackEnd;

namespace WorkerHost.Domain.Managers
{
    public interface IMatchManager
    {
        List<Match> findMatches(CompanyItem item, String token);
        String changeMatchStatus(int matchID, int statusNum);
        List<FBItem> getPostsFromGroup(String token, String GroupID);
        Match getMatchByID(int matchID);

    }
}
