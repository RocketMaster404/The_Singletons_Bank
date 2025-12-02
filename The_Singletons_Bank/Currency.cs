using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Singletons_Bank
{
    internal class Currency
    {
        private static Dictionary<string, decimal> _currencies = new Dictionary<string, decimal>()
        {
            { "SEK", 1m },
            { "USD", 0.1m },
            { "EUR", 0.08m }
        };
        public static bool AskIfToChangeCurrencyExchangeRate()
        {
            bool answer = false;
            Console.WriteLine("Vill du ändra någon av valutornas växelkurs?");

            Utilities.startColoring(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("[1]: Ja");
            Utilities.stopColoring();

            Utilities.startColoring(ConsoleColor.Red, ConsoleColor.Black);
            Console.WriteLine("[2]: Nej");
            Utilities.stopColoring();

            int stringAnswer = Utilities.GetUserNumberMinMax(1, 2);
            if(stringAnswer == 1)
            {
                answer = true;
            }
            else
            {
                answer = false;
                Console.Clear();
            }
                return answer;
        }

        public static void ChangeCurrencyExchangeRate(string currencyCode, decimal newRate)
        {
            
            if (_currencies.ContainsKey(currencyCode))
            {
                
                _currencies[currencyCode] = newRate;
                Console.WriteLine($"Växelkurs för {currencyCode} uppdaterad till {newRate}.");
            }
            else
            {
                
                Console.WriteLine($"Växelkurs för '{currencyCode}' finns inte.");
            }
        }

        public static void ChangeCurrencyExchangeRateMenu()
        {
            Console.WriteLine("vilken valutas växelkurs vill du ändra?");
            int index = 0;
            foreach (var KeyValuePair in _currencies)
            {
                
                if (KeyValuePair.Key != "SEK")
                {
                    index++;
                    Console.WriteLine($"{index}|Valuta: {KeyValuePair.Key}, Värde gemfört med SEK: {KeyValuePair.Value}");
                }
                
            }
            int answer = Utilities.GetUserNumberMinMax(1,2);
            Console.WriteLine("Vad vill du sätta den till?");
            string currencyToChange = "0";
            decimal newValue = Utilities.GetUserDecimalInput();

            //Inte direkt det bästa sätet att göra detta men det får bli så denna behöver expandera om det läggs till nya valutor
            if (answer == 1)
            {
                currencyToChange = "USD";
            }
            else if (answer == 2)
            {
                currencyToChange = "EUR";
            }

            bool foundValue = false;
            foreach (var currency in _currencies.Keys) 
            {
                if (currencyToChange == currency) 
                {
                    _currencies[currency] = newValue;
                    foundValue = true;
                    Console.WriteLine($"Valutan '{currencyToChange}' växelkurs har ändrats till {newValue}.");
                    Utilities.NoContentMsg();
                }
            }

            if (!foundValue)
            {
                Console.WriteLine("Valutan du valde finns inte i vårat system ;(");
            }
        }

        public static decimal ConvertCurrency(string currencyCode1, string currencyCode2, decimal amountToConvert)
        {
            // Denna kollar om båda valutorna som är valda finns i dictionaryn
            if (_currencies.ContainsKey(currencyCode1) && _currencies.ContainsKey(currencyCode2))
            {

                decimal rateFrom = _currencies[currencyCode1];
                decimal rateTo = _currencies[currencyCode2];

                // Omvandlar pengarna
                decimal convertedAmount = (amountToConvert / rateFrom) * rateTo;

                return convertedAmount;
            }
            else
            {
                Console.WriteLine("En eller båda valutor finns inte i vårat system.");
                return 0; 
            }
        }

        public static void DisplayExchangeRates()
        {
            Console.WriteLine("Växelkurser:");
            foreach (var currency in _currencies)
            {
                Console.WriteLine($"{currency.Key}: {currency.Value}");
            }
        }
    }
}
