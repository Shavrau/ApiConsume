using System;

namespace CurrencyConverter
{
    class Program
    {
        private static readonly string apiKey = "56b4105bb7247af9e6067ad9";

        static void Main(string[] args)
        {
            Console.WriteLine("Conversor de Moedas");

            var currencyService = new CurrencyService(apiKey);
            var userInputService = new UserInputService();

            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Converter moeda");
                Console.WriteLine("2. Ver preço de 1 unidade de moeda em Reais");
                Console.WriteLine("0. Sair");

                int option = userInputService.GetOption();

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Programa encerrado.");
                        return;
                    case 1:
                        ConvertCurrency(currencyService, userInputService);
                        break;
                    case 2:
                        ViewCurrencyRates(currencyService, userInputService);
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ConvertCurrency(CurrencyService currencyService, UserInputService userInputService)
        {
            decimal amount = userInputService.GetAmount();
            if (amount == 0)
            {
                return;
            }

            string fromCurrency = userInputService.GetCurrency("Digite a moeda de origem (ex: BRL, ARS, USD, EUR): ");
            string toCurrency = userInputService.GetCurrency("Digite a moeda de destino (ex: BRL, ARS, USD, EUR): ");

            try
            {
                decimal convertedAmount = currencyService.ConvertCurrency(amount, fromCurrency, toCurrency).Result;
                Console.WriteLine($"{amount} {fromCurrency} é equivalente a {convertedAmount} {toCurrency}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        static void ViewCurrencyRates(CurrencyService currencyService, UserInputService userInputService)
        {
            string currency = userInputService.GetCurrency("Digite a moeda (ex: BRL, ARS, USD, EUR): ");

            try
            {
                decimal rate = currencyService.GetExchangeRate(currency, "BRL").Result;
                Console.WriteLine($"1 {currency} é equivalente a {rate} BRL");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter taxa de câmbio: {ex.Message}");
            }
        }
    }
}
