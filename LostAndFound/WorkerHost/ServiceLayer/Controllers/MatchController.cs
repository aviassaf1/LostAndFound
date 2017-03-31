using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.Domain.Managers;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost.ServiceLayer.Controllers
{
    class MatchController : IMatchController
    {
        private static IMatchController singleton;
        private static IMatchManager IMM; 
        private MatchController()
        {
            IMM = MatchManager.getInstance;
        }

        public static IMatchController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new MatchController();
                }
                return singleton;
            }
        }

        public string changeMatchStatus(int matchID, string statusNum)
        {
            int num = 1;
            if (statusNum.Equals("אפשרי"))
                num = 1;
            else if (statusNum.Equals("נכון"))
                num = 2;
            else if (statusNum.Equals("הושלם"))
                num = 3;
            else if (statusNum.Equals("לא נכון"))
                num = 4;
            return IMM.changeMatchStatus(matchID, num);
        }

        public List<MatchData> getMatchesByItemID(int itemID)
        {
            throw new NotImplementedException();//TODO: implement function
        }
    }
}
