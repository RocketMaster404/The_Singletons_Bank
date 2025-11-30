using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Utilities
    {
        public static decimal GetUserDecimal()
        {
            decimal input;
            while (!Decimal.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Du måste ange ett giltigt tal!");
            }
            return input;
        }
        public static void DashDivide()
        {
            Console.WriteLine("_________________________________________________________________________________________");
        }
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
        public static void NoContentMsg()
        {
            DashDivide();
            Console.WriteLine("\nTryck på valfri tangent för att återgå till huvudmenyn");
            Console.ReadLine();
            Console.Clear();
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

        public static decimal GetUserDecimalInput()
        {
            decimal input;
            while (!decimal.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Du måste ange ett decimaltal.");
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

        public static void AsciiArtPrinter(bool animationOn)
        {

            string[] art = new string[]
            {
                @" _____ _             _      _                   ______             _    ",
                @"/  ___(_)           | |    | |                  | ___ \           | |   ",
                @"\ `--. _ _ __   __ _| | ___| |_ ___  _ __  ___  | |_/ / __ _ _ __ | | __",
                @" `--. \ | '_ \ / _` | |/ _ \ __/ _ \| '_ \/ __| | ___ \/ _` | '_ \| |/ /",
                @"/\__/ / | | | | (_| | |  __/ || (_) | | | \__ \ | |_/ / (_| | | | |   < ",
                @"\____/|_|_| |_|\__, |_|\___|\__\___/|_| |_|___/ \____/ \__,_|_| |_|_|\_\",
                @"                __/ |                                                   ",
                @"               |___/                                                    "
            };


            // Hittar den längsta stringen i arrayen för att veta när den ska sluta
            int maxWidth = art.Max(line => line.Length);
            Console.CursorVisible = false;
            if (animationOn)
            {
                for (int currentWidth = 1; currentWidth <= maxWidth; currentWidth++)
                {
                    // Bättre än Console.clear()
                    Console.SetCursorPosition(0, 0);

                    // Skriver ut stringen från en bokstav tills hella texten har skrivits ut som gör en liten cool animation
                    foreach (string line in art)
                    {
                        int lengthToShow = Math.Min(currentWidth, line.Length);
                        Console.WriteLine(line.Substring(0, lengthToShow));
                    }

                    Thread.Sleep(1);
                }
            }
            else
            {
                foreach (string line in art)
                {
                    
                    Console.WriteLine(line.Substring(0, maxWidth));
                }
            }
            
        }
    }
}
