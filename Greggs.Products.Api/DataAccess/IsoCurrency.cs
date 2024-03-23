using System.Collections.Generic;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

public static class IsoCurrency
{
    public const string GbpCurrencyCode = "GBP";
    public const string EurCurrencyCode = "EUR";

    public static readonly IReadOnlyCollection<string> SupportedCurrencyCodes = new List<string>()
        { GbpCurrencyCode, EurCurrencyCode };

    public static readonly Currency Gbp = new() { Code = GbpCurrencyCode, Name = "British Pound Sterling" };
    public static readonly Currency Eur = new() { Code = EurCurrencyCode, Name = "Euro" };
}
