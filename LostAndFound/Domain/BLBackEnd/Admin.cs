using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    class Admin : User
    {
        public Admin(String adminName, String password)
        {
            _userName = adminName;
            _password = password;
        }
    }
}
