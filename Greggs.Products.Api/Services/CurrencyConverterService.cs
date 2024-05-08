using System;
using Greggs.Products.Api.DataAccess;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Services;

public class CurrencyConverterService : ICurrencyConverterService
{
    private readonly ILogger<CurrencyConverterService> _logger;
    private readonly IExchangeRateAccess _exchangeRateRepository;

    public CurrencyConverterService(ILogger<CurrencyConverterService> logger,
        IExchangeRateAccess exchangeRateRepository)
    {
        _logger = logger;
        _exchangeRateRepository = exchangeRateRepository;
    }

    public decimal ConvertCurrency(decimal amount, string isoCurrencyFrom, string isoCurrencyTo, DateTime? date = null)
    {
        try
        {
            var exchangeRate = _exchangeRateRepository.GetExchangeRate(isoCurrencyFrom, isoCurrencyTo, date);
            return amount * exchangeRate.Rate;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unable to convert currency {isoCurrencyFrom} to {isoCurrencyTo}",
                isoCurrencyFrom, isoCurrencyTo);
            throw;
        }
    }
}
