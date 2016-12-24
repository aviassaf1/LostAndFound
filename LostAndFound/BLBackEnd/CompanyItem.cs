using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class CompanyItem : Item
    {
        private int _serialNumber;
        private String _companyName;
        private String _contactName;
        private String _contactPhone;
        private String _photoLocation;
    }
}
