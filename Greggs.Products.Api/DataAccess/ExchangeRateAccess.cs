using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

public class ExchangeRateAccess : IExchangeRateAccess
{
    private static readonly IEnumerable<ExchangeRate> ExchangeRateDatabase = new List<ExchangeRate>
    {
        new() { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2024, 3, 23), Rate = 1.11m },
        new() { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2023, 3, 23), Rate = 1.21m }
    };

    public ExchangeRate GetExchangeRate(string isoCurrencyFrom, string isoCurrencyTo, DateTime? date = null)
    {
        var queryable = ExchangeRateDatabase.AsQueryable();

        var exchangeRates = queryable.Where(currency =>
            currency.From.Code == isoCurrencyFrom && currency.To.Code == isoCurrencyTo);

        if (date != null)
        {
            exchangeRates = exchangeRates.Where(currency => currency.Date == date);
        }

        if (!exchangeRates.Any())
        {
            throw new Exception(
                $"Exchange rate not found for {isoCurrencyFrom} to {isoCurrencyTo} at date {date ?? DateTime.Now}");
        }

        return exchangeRates.OrderByDescending(currency => currency.Date).FirstOrDefault();
    }
}
