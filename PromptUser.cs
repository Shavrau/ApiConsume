namespace CurrencyConverter
{
    public class UserInputService
    {
        private static readonly string[] ValidCurrencies = { "BRL", "ARS", "USD", "EUR" };

        public decimal GetAmount()
        {
            Console.Write("Digite o valor (ou 0 para sair): ");
            while (true)
            {
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    return amount;
                }
                Console.Write("Valor inválido. Digite um valor numérico: ");
            }
        }

        public string GetCurrency(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string currency = Console.ReadLine().ToUpper();
                if (Array.Exists(ValidCurrencies, element => element == currency))
                {
                    return currency;
                }
                Console.Write($"Moeda inválida. {prompt}");
            }
        }
    }
}
