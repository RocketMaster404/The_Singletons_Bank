using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Bank
    {
        private static Dictionary<string, string> _users = new Dictionary<string, string>();
       

        public static void AddUser(string userName, string password)
        {
            if (userExists)
            {
                // felmeddelande
                return;
            }
            _users.Add()
        }

       
    }
}
