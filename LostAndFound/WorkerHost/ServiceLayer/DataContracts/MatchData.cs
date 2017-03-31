using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    class MatchData
    {
        [DataMember]
        private int _matchID;
        [DataMember]
        private int _companyItemID;
        [DataMember]
        private int _item2ID;
        [DataMember]
        private string _matchStatus;
    }
}
