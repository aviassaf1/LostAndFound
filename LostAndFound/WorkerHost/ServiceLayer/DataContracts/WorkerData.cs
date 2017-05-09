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
        private bool isAdmin;

        public WorkerData(string name, bool is_admin)
        {
            userName = name;
            isAdmin = is_admin;
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

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }
    }
}
