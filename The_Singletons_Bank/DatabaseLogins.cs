using System.IO;

namespace The_Singletons_Bank
{
    internal class DatabaseLogins
    {
        static string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Logins.txt";

        public static void AddAllLogins(List<User> users)
        {
            List<string> accounts = ReadFile();
            List<string> newAccounts = NewAccounts(users);
            List<string> accountsToAdd = new List<string>();

            foreach (string newUser in newAccounts)
            {
                if (!accounts.Contains(newUser))
                {
                    accountsToAdd.Add(newUser);
                }
            }
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (string line in accountsToAdd)
                {
                    writer.WriteLine(line);
                }
            }
        }
        public static void UpdateLogins(List<User> users)
        {
            string line = "";

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();

                    string[] parts = line.Split(';');

                    string username = parts[0];
                    string password = parts[1];
                    string isAdmin = parts[4];

                    if (isAdmin == "False")
                    {
                        var acc = new Customer(username, password);

                        if (!Bank.userExists(acc.GetUsername()))
                        {
                            Bank.AddUser(acc);
                        }
                    }
                    else if (isAdmin == "True")
                    {
                        var acc = new Admin(username, password, true);

                        if (!Bank.userExists(acc.GetUsername()))
                        {
                            Bank.AddUser(acc);
                        }
                    }
                }
            }
        }
        public static List<string> ReadFile()
        {
            List<string> accounts = new List<string>();

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    accounts.Add(reader.ReadLine());
                }
                reader.Close();
            }
            return accounts;
        }
        public static List<string> NewAccounts(List<User> users)
        {
            List<string> accounts = new List<string>();

            foreach (User user in users)
            {
                accounts.Add(GetUserString(user));
            }
            return accounts;
        }
        public static string GetUserString(User user)
        {
            return $"{user.GetUsername()};{user.GetPassword()};{user.LoginAttempts};{user.UserIsBlocked};{user.IsAdmin}";
        }
    }
}