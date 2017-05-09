using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    public class GroupData
    {
        [DataMember]
        private string _groupName;
        [DataMember]
        private string _groupID;

        public GroupData(string groupName, string groupID)
        {
            _groupName = groupName;
            _groupID = groupID;
        }

        public string GroupName
        {
            get
            {
                return _groupName;
            }

            set
            {
                _groupName = value;
            }
        }

        public string GroupID
        {
            get
            {
                return _groupID;
            }

            set
            {
                _groupID = value;
            }
        }
    }
}
