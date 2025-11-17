using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Admin:User
    {
        public bool IsAdmin = true;



        public Admin(string username, string password) : base(username, password)
        {

        }
    }
}
