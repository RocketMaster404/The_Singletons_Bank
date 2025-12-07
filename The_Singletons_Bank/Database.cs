namespace The_Singletons_Bank
{
    internal class Database
    {
        public static void WriteToFile(List<User> input)
        {
            string path = "C:\\Users\\liamb\\Desktop\\Shared Code\\Text.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (User user in input)
                {
                    writer.WriteLine($"{user.GetUsername()};{user.GetPassword()};{user.LoginAttempts};{user.UserIsBlocked};{user.IsAdmin}");
                }
            }
        }
    }
}
