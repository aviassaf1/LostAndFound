using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.BLBackEnd
{
    public abstract class User
    {
        protected static Cache cache = Cache.getInstance;
        protected String _userName;
        protected String _password;

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                cache.updateUser(_userName, _password);
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
        }

    }
}
