using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    public abstract class User
    {
        //private string _username;//Behövs detta??????

        public string Username { get; set; }

        public string Password { get; set; } 

        public int LoginAttempts { get; set; } = 0;

        public bool UserIsBlocked { get; set; } = false;

        public bool IsAdmin { get; set; } = false;


        
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            
        }

        public User(string username, string password, bool isadmin)
        {
            Username = username;
            Password = password;
            IsAdmin = isadmin;
        }

       public bool Admincheck(string password, string username)
        {
            if (Password == password && Username == username && IsAdmin==true)
            {
                return true;
            }
            return false;
        }

        public  bool Logincheck(string password, string username)
        {
            
            if (Password == password && Username==username )
            {
                return true;
            }

            return false;
        }
        
        
    }
}
