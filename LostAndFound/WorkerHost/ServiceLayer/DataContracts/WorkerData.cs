using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    public class WorkerData
    {
        [DataMember]
        private string userName;
        [DataMember]
        private bool isManager;

        public WorkerData(string name, bool is_manger)
        {
            userName = name;
            isManager = is_manger;
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public bool IsManager
        {
            get
            {
                return isManager;
            }

            set
            {
                isManager = value;
            }
        }
    }
}
