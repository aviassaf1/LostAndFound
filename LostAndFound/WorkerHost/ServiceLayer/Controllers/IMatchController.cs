﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WorkerHost.ServiceLayer.DataContracts;

namespace WorkerHost.ServiceLayer.Controllers
{
    [ServiceContract]
    interface IMatchController
    {
        [OperationContract]
        String changeMatchStatus(int matchID, string statusNum);
        [OperationContract]
        List<MatchData> getMatchesByItemID(int itemID);
    }
}
