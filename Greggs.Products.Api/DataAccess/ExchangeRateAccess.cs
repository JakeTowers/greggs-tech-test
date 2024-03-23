using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

public class ExchangeRateAccess : IExchangeRateAccess
{
    private static readonly IEnumerable<ExchangeRate> ExchangeRateDatabase = new List<ExchangeRate>
    {
        new() { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2024, 3, 23), Rate = 1.11m }
    };

    public ExchangeRate GetExchangeRate(string isoCurrencyFrom, string isoCurrencyTo, DateTime? date = null)
    {
        var queryable = ExchangeRateDatabase.AsQueryable();

        var currencyPair = queryable.Where(currency =>
            currency.From.Code == isoCurrencyFrom && currency.To.Code == isoCurrencyTo);

        if (date != null)
        {
            currencyPair = currencyPair.Where(currency => currency.Date == date);
        }

        if (!currencyPair.Any())
        {
            throw new Exception($"Currency pair not found for {isoCurrencyFrom} and {isoCurrencyTo} at date {date}");
        }

        return currencyPair.OrderByDescending(currency => currency.Date).FirstOrDefault();
    }
}
