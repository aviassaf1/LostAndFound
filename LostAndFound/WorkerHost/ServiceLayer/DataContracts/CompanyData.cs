using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WorkerHost.ServiceLayer.DataContracts
{
    [DataContract]
    public class CompanyData
    {
        [DataMember]
        private string companyName;
        [DataMember]
        private string phoneNumber;

        public CompanyData(string name, string ph)
        {
            companyName = name;
            phoneNumber = ph;
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }

            set
            {
                companyName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }

            set
            {
                phoneNumber = value;
            }
        }
    }
}
