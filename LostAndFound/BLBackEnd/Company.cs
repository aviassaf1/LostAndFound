using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    class Company : User
    {
        private String _companyName;
        private String _phone;
        private HashSet<int> _lostItems;
        private HashSet<int> _foundItems;
        private HashSet<int> _matches;
    }
}
