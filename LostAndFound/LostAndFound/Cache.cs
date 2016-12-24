using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound
{
    class Cache
    {
        //private Dictionary<string, User> _users;
        private static Cache singleton;

        private Cache()
        {
            //_users = new Dictionary<string, User>();
        }

        public static Cache getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Cache();
                }
                return singleton;
            }
        }

        public void clear()
        {
//            if (this._forums != null)
//                this._forums.Clear();            

        }
    }
}
