using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class CompanyItem : Item
    {
        protected int _serialNumber;
        protected String _companyName;
        protected String _contactName;
        protected String _contactPhone;
        protected String _photoLocation;

        public int SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                _serialNumber = value;
                cache.updateCompanyItem(this);
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
                cache.updateCompanyItem(this);
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
                cache.updateCompanyItem(this);
            }
        }

        public string PhotoLocation
        {
            get
            {
                return _photoLocation;
            }

            set
            {
                _photoLocation = value;
                cache.updateCompanyItem(this);
            }
        }

        protected string CompanyName
        {
            get
            {
                return _companyName;
            }
        }
    }
}
