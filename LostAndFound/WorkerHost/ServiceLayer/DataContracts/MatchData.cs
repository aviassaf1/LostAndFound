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

        public int MatchID
        {
            get
            {
                return _matchID;
            }

            set
            {
                _matchID = value;
            }
        }

        public int CompanyItemID
        {
            get
            {
                return _companyItemID;
            }

            set
            {
                _companyItemID = value;
            }
        }

        public int Item2ID
        {
            get
            {
                return _item2ID;
            }

            set
            {
                _item2ID = value;
            }
        }

        public string MatchStatus
        {
            get
            {
                return _matchStatus;
            }

            set
            {
                _matchStatus = value;
            }
        }

        public MatchData(int matchID,int companyItemID,int item2ID,string status)
        {
            MatchID = matchID;
            CompanyItemID = companyItemID;
            Item2ID = item2ID;
            MatchStatus = status;
        }
    }
}
