using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    public abstract class CompanyItem : Item
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

        public string CompanyName
        {
            get
            {
                return _companyName;
            }
        }

        internal string updateItem(DateTime date, string location, string description, int serialNumber, string contactName,
            string contactPhone)
        {
            _date = date;
            _location = location;
            _description = description;
            _serialNumber = serialNumber;
            _contactName = contactName;
            _contactPhone = contactPhone;
            return cache.updateCompanyItem(this);
        }
    }
}
