using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    class Company
    {
        String _companyName;
        String _phone;
        HashSet<int> _lostItems;
        HashSet<int> _foundItems;
        HashSet<int> _matches;
    }
}
