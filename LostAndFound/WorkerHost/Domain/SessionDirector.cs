using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerHost.Domain
{
    class SessionDirector
    {
        private static SessionDirector singleton;
        private Dictionary<int, String> _sessions = new Dictionary<int, string>();//key, username
        private Dictionary<int, String> _adminSessions = new Dictionary<int, string>();//key, username

        private SessionDirector()
        {
        }
        public static SessionDirector getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SessionDirector();
                }
                return singleton;
            }
        }

        public int generateKey(String username)
        {
            int res = generate();
            _sessions.Add(res, username);
            return res;
        }

        public int generateAdminKey(String adminName)
        {
            int res = generate();
            _adminSessions.Add(res, adminName);
            return res;
        }
        private int generate()
        {
            Random random = new Random();
            int result = random.Next(10000000, 100000000);
            while (_sessions.Keys.Contains(result) && _adminSessions.Keys.Contains(result))
            {
                result = random.Next(10000000, 100000000);
            }
            return result;
        }

        public String getAdminName(int key)
        {
            if (_adminSessions.Keys.Contains(key))
            {
                return (_adminSessions[key]);
            }
            else
            {
                return null;
            }
        }

        public String getUserName(int key)
        {
            if (_sessions.Keys.Contains(key))
            {
                return (_sessions[key]);
            }
            else
            {
                return null;
            }
        }
    }
}
