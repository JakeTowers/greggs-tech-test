using System;

namespace Greggs.Products.Api.Services;

public interface ICurrencyConverterService
{
    decimal ConvertCurrency(decimal amount, string isoCurrencyFrom, string isoCurrencyTo, DateTime? date = null);
}
