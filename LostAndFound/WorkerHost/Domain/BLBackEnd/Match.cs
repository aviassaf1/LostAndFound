using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain.BLBackEnd
{
    public class Match
    {        
        private int _matchID;
        private int _companyItemID;
        private int _item2ID;
        private MatchStatus _matchStatus;

        public Match(int matchID, int companyItemID, int item2ID, MatchStatus matchStatus)
        {
            _matchID = matchID;
            _companyItemID = companyItemID;
            _item2ID = item2ID;
            _matchStatus = matchStatus;
        }
        public Match(int companyItemID, int item2ID, MatchStatus matchStatus)
        {
            _matchID = -1;
            _companyItemID = companyItemID;
            _item2ID = item2ID;
            _matchStatus = matchStatus;
        }

        public void addToDB()
        {
            Cache.getInstance.addMatch(this);
        }

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

        internal string delete()
        {
            return Cache.getInstance.deleteMatch(MatchID);
        }

        public MatchStatus MatchStatus
        {
            get
            {
                return _matchStatus;
            }


            set
            {
                _matchStatus = value;
                Cache.getInstance.updateMatch(_matchID, _matchStatus);
            }
        }
    }
}
