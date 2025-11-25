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

        public static void AsciiArtPrinter(bool animationOn)
        {
            // this @ mark will make so / and other symbols dont create error
            string asciiArt = @" _____ _                  _ _                   ______             _    
/  ___(_)                | | |                  | ___ \           | |   
\ `--. _ _ __   __ _  ___| | |_ ___  _ __  ___  | |_/ / __ _ _ __ | | __
 `--. \ | '_ \ / _` |/ _ \ | __/ _ \| '_ \/ __| | ___ \/ _` | '_ \| |/ /
/\__/ / | | | | (_| |  __/ | || (_) | | | \__ \ | |_/ / (_| | | | |   < 
\____/|_|_| |_|\__, |\___|_|\__\___/|_| |_|___/ \____/ \__,_|_| |_|_|\_\
                __/ |                                                   
               |___/                                                    ";

            //This will split up the ascciart into lines 
            string[] lines = asciiArt.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Console.CursorVisible = false;

            //If animationOn == true then the animation will play out otherwise just a static image
            if(animationOn == true)
            {
                foreach (string line in lines)
                {
                    // This will loop throughout the line, increasing 'i' by 5 every time
                    for (int i = 0; i < line.Length; i += 5)
                    {
                        // This will count how many characters are left
                        int remaining = line.Length - i;

                        // This will make count into 5 or whatever is remaining
                        int count = Math.Min(5, remaining);

                        // Outputs the text
                        Console.Write(line.Substring(i, count));

                        // Pause
                        Thread.Sleep(10);
                    }

                    // Move to next line
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine(asciiArt);
            }
            
        }
    }
}
