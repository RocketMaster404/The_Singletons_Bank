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
            Console.WriteLine("Skriv in den 3 bokstavliga förkortningen för valutan du vill ändra");
            string currencyToChange = Console.ReadLine();
            Console.WriteLine("Vad vill du sätta den till?");
            decimal newValue = Utilities.GetUserNumberDecimal();
            bool foundValue = false;
            foreach (var currency in _currencies.Keys) 
            {
                if (currencyToChange == currency) 
                {
                    _currencies[currency] = newValue;
                    foundValue = true;
                    Console.WriteLine($"Currency '{currencyToChange}' value updated to {newValue}.");
                }
            }

            if (!foundValue)
            {
                Console.WriteLine("Valutan du valde finns inte i vårat system ;(");
            }
        }

        public static decimal ConvertCurrency(string currencyCode1, string currencyCode2, decimal amountToConvert)
        {
            // Check if both currency codes exist in the dictionary
            if (_currencies.ContainsKey(currencyCode1) && _currencies.ContainsKey(currencyCode2))
            {
                // Retrieve the exchange rates
                decimal rateFrom = _currencies[currencyCode1];
                decimal rateTo = _currencies[currencyCode2];

                // Convert the amount based on the exchange rates
                decimal convertedAmount = (amountToConvert / rateFrom) * rateTo;

                return convertedAmount; // Return the converted amount
            }
            else
            {
                Console.WriteLine("One or both currency codes do not exist.");
                return 0; // Return 0 or throw an exception based on how you want to handle errors
            }
        }

        public static void DisplayExchangeRates()
        {
            Console.WriteLine("Current Exchange Rates:");
            foreach (var currency in _currencies)
            {
                Console.WriteLine($"{currency.Key}: {currency.Value}");
            }
        }
    }
}
