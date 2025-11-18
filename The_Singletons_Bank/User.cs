using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    public abstract class User
    {
        private string _username;//Behövs detta??????

        public string Username { get; set; }

        public string Password { get; set; }

        public int LoginAttempts { get; set; } = 0;

        public bool UserIsBlocked { get; set; } = false;
        protected User(string username, string password)
        {
            Username = username;
            Password = password;
            
        }
        
        
    }
}
