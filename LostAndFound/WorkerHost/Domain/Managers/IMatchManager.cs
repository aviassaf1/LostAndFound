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
        String changeMatchStatus(int matchID, int statusNum, int key);
        List<Match> getMatchesByItemID(int itemID, int key);
        Match getMatchByID(int matchID);

    }
}
