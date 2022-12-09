using System.Globalization;
using System.Xml;

namespace API
{
    // found on https://learn.microsoft.com/en-us/answers/questions/785478/api-for-currency-exchange-rate.html
    public static class Currency
    {
        // prices referenced to EUR
        static readonly Dictionary<string, decimal> ExchangeRatesEURref = new();

        public static decimal? GetRate(string currency) =>
            ExchangeRatesEURref.TryGetValue(currency, out decimal value) ? value : null;

        public static void LoadRates()
        {
            XmlDocument xmlDoc = new();
            xmlDoc.Load("http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml");

            using var data = xmlDoc?.DocumentElement?.ChildNodes[2]?.ChildNodes[0]?.ChildNodes;

            if (data == null) return;

            foreach (XmlNode node in data)
            {
                string? currency = node?.Attributes?["currency"]?.Value;
                string? rate = node?.Attributes?["rate"]?.Value;

                if (currency == null || rate == null) continue;

                decimal rateConverted = decimal.Parse(rate, CultureInfo.InvariantCulture);

                ExchangeRatesEURref.Add(currency, rateConverted);
            }
        }

        public static string[] GetCurrencies => ExchangeRatesEURref.Keys.ToArray();

    }
}