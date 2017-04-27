using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    public class MatchData
    {
        [DataMember]
        private int _matchID;
        [DataMember]
        private int _companyItemID;
        [DataMember]
        private int _item2ID;
        [DataMember]
        private string _matchStatus;

        [DataMember]
        private List<string> _colors;
        [DataMember]
        private DateTime _date;
        [DataMember]
        private string _location;
        [DataMember]
        private string _description;




        public string Colors
        {
            get
            {
                string str = "";
                foreach (string color in _colors)
                {
                    str = str + "," + color;
                }
                if (!str.Equals(""))
                {
                    str = str.Substring(1);
                }
                return str;
            }
        }

        public String Date
        {
            get
            {
                return _date.ToString("dd/MM/yyyy");
            }
        }

        public string Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
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

        public MatchData(int matchID,int companyItemID,int item2ID,string status, 
            List<string> colors, string location, DateTime date, string desc)
        {
            MatchID = matchID;
            CompanyItemID = companyItemID;
            Item2ID = item2ID;
            MatchStatus = status;
            _colors = colors;
            Location = location;
            _date = date;
            Description = desc;
        }
    }
}
