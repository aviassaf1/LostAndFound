using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BLBackEnd;

namespace Domain.Managers
{
    public interface IMatchManager
    {
        List<Match> findMatches(CompanyItem item, String token);
        String changeMatchStatus(int matchID, int statusNum);

    }
}
