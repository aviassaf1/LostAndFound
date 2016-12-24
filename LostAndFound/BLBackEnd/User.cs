using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLBackEnd
{
    abstract class User
    {
        protected String _userName;
        protected String _password;

        protected string UserName
        {
            get
            {
                return _userName;
            }
        }

        protected string Password
        {
            get
            {
                return _password;
            }
        }
    }
}
