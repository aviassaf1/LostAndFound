using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    class Match
    {
        private static Cache cache = Cache.getInstance;

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

        public void addToDB()
        {
            cache.addMatch(this);
        }

        public int MatchID
        {
            get
            {
                return _matchID;
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

        internal MatchStatus MatchStatus
        {
            get
            {
                return _matchStatus;
            }

            set
            {
                _matchStatus = value;
                cache.updateMatch(_matchID, _matchStatus);
            }
        }
    }
}
