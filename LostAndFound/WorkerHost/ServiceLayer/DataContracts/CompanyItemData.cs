using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    public class CompanyItemData
    {
        [DataMember]
        protected int _itemID;
        [DataMember]
        protected List<string> _colors;
        [DataMember]
        protected string _itemType;
        [DataMember]
        protected DateTime _date;
        [DataMember]
        protected String _location;
        [DataMember]
        protected String _description;
        [DataMember]
        protected int _serialNumber;
        [DataMember]
        protected String _companyName;
        [DataMember]
        protected String _contactName;
        [DataMember]
        protected String _contactPhone;
        [DataMember]
        protected bool _status;
        [DataMember]
        protected string _type;

        public CompanyItemData(int itemID, List<string> colors, string itemType, string location, DateTime date, string desc,
            int serNum, string comName, string conName, string conPhone, bool stat, string type)
        {
            _itemID = itemID;
            _colors = colors;
            _itemType = itemType;
            _location = location;
            _date = date;
            _description = desc;
            _serialNumber = serNum;
            _companyName = comName;
            _contactName = conName;
            _contactPhone = conPhone;
            _status = stat;
            _type = type;
        }
    }
}