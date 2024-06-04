using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter
{
    class Program
    {
        private static readonly string apiKey = "56b4105bb7247af9e6067ad9";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Conversor de Moedas");

            var currencyService = new CurrencyService(apiKey);
            var userInputService = new UserInputService();

            while (true)
            {
                decimal amount = userInputService.GetAmount();
                if (amount == 0)
                {
                    break;
                }

                string fromCurrency = userInputService.GetCurrency("Digite a moeda de origem (ex: BRL, ARS, USD, EUR): ");
                string toCurrency = userInputService.GetCurrency("Digite a moeda de destino (ex: BRL, ARS, USD, EUR): ");

                try
                {
                    decimal convertedAmount = await currencyService.ConvertCurrency(amount, fromCurrency, toCurrency);
                    Console.WriteLine($"{amount} {fromCurrency} é equivalente a {convertedAmount} {toCurrency}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Programa encerrado.");
        }
    }
}
