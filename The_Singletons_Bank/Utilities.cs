using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Utilities
    {
        public static string GetUserChoiceYN()
        {
            while (true)
            {
                Console.WriteLine("Ange (Y) eller (N)");
                string choice = Console.ReadLine().ToLower();

                if (choice == "y")
                {

                    return "y";

                }
                else if (choice == "n")
                {
                    return "n";
                }
            }
        }

        public static int GetUserNumberMinMax(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.WriteLine($"Du måste ange ett tal mellan {min}-{max}");
            }
            return input;
        }

        public static int GetUserNumber()
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine($"Du måste ange ett heltal");
            }
            return input;

        }

        public static int GetUserNumbermsg(string msg)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine(msg);
            }
            return input;

        }

        public static void startColoring(ConsoleColor frontColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = frontColor;
            Console.BackgroundColor = backgroundColor;
        }

        public static void stopColoring()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

    }
}
