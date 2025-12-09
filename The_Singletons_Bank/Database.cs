namespace The_Singletons_Bank
{
    internal class Database
    {
        public static void AddAccountsToFile(List<User> input)
        {            
            string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Text.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                foreach (User user in input)
                {
                    writer.WriteLine($"{user.GetUsername()};{user.GetPassword()};{user.LoginAttempts};{user.UserIsBlocked};{user.IsAdmin}");
                }
                writer.Close();
            }
        }
        public static void AddUserToFile(string user)
        {
            string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Text.txt";

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(user);
                writer.Close();
            }
        }
        public static void AddAccountCheck(string input)
        {
            List<string> usersCheck = new List<string>();

            int count = 0;

            string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Text.txt";

            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    usersCheck.Add(reader.ReadLine());
                }
                reader.Close();
            }
            foreach (string user in usersCheck)
            {
                if (input != user)
                {
                    count++;
                }
                if (count == usersCheck.Count)
                {
                    AddUserToFile(input);
                }
                
            }

        }        
        public static string GetUserString(User user)
        {
            return $"{user.GetUsername()};{user.GetPassword()};{user.LoginAttempts};{user.UserIsBlocked};{user.IsAdmin}";
        }
    }
}
