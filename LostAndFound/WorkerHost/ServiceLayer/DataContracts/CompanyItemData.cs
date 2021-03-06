﻿using System;
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
        private int _itemID;
        [DataMember]
        private List<string> _colors;
        [DataMember]
        private string _itemType;
        [DataMember]
        private DateTime _date;
        [DataMember]
        private String _location;
        [DataMember]
        private String _description;
        [DataMember]
        private int _serialNumber;
        [DataMember]
        private String _companyName;
        [DataMember]
        private String _contactName;
        [DataMember]
        private String _contactPhone;
        [DataMember]
        private bool _status;
        [DataMember]
        private string _type;

        public int ItemID
        {
            get
            {
                return _itemID;
            }

            set
            {
                _itemID = value;
            }
        }

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

        public string ItemType
        {
            get
            {
                return _itemType;
            }

            set
            {
                _itemType = value;
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

        public int SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                _serialNumber = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return _companyName;
            }

            set
            {
                _companyName = value;
            }
        }

        public string ContactName
        {
            get
            {
                return _contactName;
            }

            set
            {
                _contactName = value;
            }
        }

        public string ContactPhone
        {
            get
            {
                return _contactPhone;
            }

            set
            {
                _contactPhone = value;
            }
        }

        public String Status
        {
            get
            {
                if (_status) { 
                    if(_type.Equals("found"))
                        return "נמסר";
                    else if (_type.Equals("lost"))
                        return "נמצא";
                }
                else 
                {
                    if (_type.Equals("found"))
                        return "לא נמסר";
                    else if (_type.Equals("lost"))
                        return "לא נמצא";
                }
                return "";
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        public DateTime Date1
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
            }
        }

        public string HebType
        {
            get
            {
                if (_type.Equals("lost"))
                {
                    return "אבידה";
                }
                else
                {
                    return "מציאה";
                }
            }
        }

        public CompanyItemData(int itemID, List<string> colors, string itemType, string location, DateTime date, string desc,
            int serNum, string comName, string conName, string conPhone, bool stat, string type)
        {
            ItemID = itemID;
            _colors = colors;
            ItemType = itemType;
            Location = location;
            _date = date;
            Description = desc;
            SerialNumber = serNum;
            CompanyName = comName;
            ContactName = conName;
            ContactPhone = conPhone;
            _status = stat;
            Type = type;
        }
    }
}