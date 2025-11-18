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

        
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            
        }

       

        public  bool PasswordCheck(string password)
        {
            //Console.WriteLine($"{password}");
            if (Password == password)
            {
                return true;
            }

            return false;
        }
        
        
    }
}
