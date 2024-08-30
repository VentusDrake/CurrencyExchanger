using System;
using CurrencyConventer;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    class Program
    {
        static async Task Main(String[] args)
        {
            /// <summary>
            /// Free API allows only changing rates from USD to every other
            /// </summary>
            String apiKey = "2fb045b979494ca0843e123c2c006210";
            String apiUrl = $"https://openexchangerates.org/api/latest.json?app_id={apiKey}";
            
            String data = await GetDataAsync(apiUrl);
            JObject json = JObject.Parse(data);

            Console.WriteLine("=====================================");
            Console.WriteLine("Welcome to currency converter!");
            Console.WriteLine("Your money is in USD!");
            Console.WriteLine("=====================================");

            Console.WriteLine("To which currency you want convert?");
            showCurrencies();
            Currency responseCurrency = (Currency)(Convert.ToInt32(Console.ReadLine()) - 1);

            Console.WriteLine("How much do you want to convert: ");
            double amountToConvert = Convert.ToDouble(Console.ReadLine());

            double currencyRate = json["rates"][responseCurrency.ToString()].Value<double>();
            double convertedAmount = CurrencyUtility.Convert(amountToConvert, currencyRate);

            Console.WriteLine($"You have {convertedAmount}{responseCurrency}");
        }

        static async Task<String> GetDataAsync(String apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    String responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;

                }
                catch (HttpRequestException e)
                {
                    return $"Wystąpił błąd podczas wysyłania żądania {e.Message}";
                }
            }
        }

        static void showCurrencies()
        {
            var currencies = Enum.GetValues(typeof(Currency));
            int index = 1;
            foreach (var currency in currencies)
            {
                Console.WriteLine($"{index}) {currency}");
                index++;
            }
        }
    }

    enum Currency
    {
        EUR,
        GBP,
        PLN,
        JPY,
        AUD,
        CAD,
        CHF,
        CNY
    }
}