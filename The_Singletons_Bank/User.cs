using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    public abstract class User
    {
        private string _username;

        private string _password { get; set; }

        public int LoginAttempts { get; set; } = 3;

        public bool UserIsBlocked { get; set; } = false;

        public bool IsAdmin { get; set; } = false;


        public User(string username, string password)
        {
            _username = username;
            _password = password;

        }

        public User(string username, string password, bool isadmin)
        {
            _username = username;
            _password = password;
            IsAdmin = isadmin;
        }

        public bool Logincheck(string password, string username)
        {

            if (_password == password && _username == username)
            {
                return true;
            }

            return false;
        }
        public string GetUsername()
        {
            return _username;
        }
        public string GetPassword()
        {
            return _password;
        }
    }
}
