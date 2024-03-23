using System;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.DataAccess;

public interface IExchangeRateAccess
{
    ExchangeRate GetExchangeRate(string isoCurrencyFrom, string isoCurrencyTo, DateTime? date);
}
