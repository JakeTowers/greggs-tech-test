using System;
using FluentAssertions;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ExchangeRateAccessTests
{
    private readonly ExchangeRateAccess _exchangeRateRepository = new();

    [Fact]
    public void ExchangeRateAccess_GbpToEur_NoDateSpecified_ReturnsLatestExchangeRate()
    {
        ExchangeRate expectedValue = new()
            { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2024, 3, 23), Rate = 1.11m };

        var actualValue =
            _exchangeRateRepository.GetExchangeRate(IsoCurrency.GbpCurrencyCode, IsoCurrency.EurCurrencyCode);

        actualValue.Should().BeEquivalentTo(expectedValue);
    }

    [Fact]
    public void ExchangeRateAccess_GbpToEur_2023_03_23_ReturnsExchangeRateOfSpecifiedDate()
    {
        ExchangeRate expectedValue = new()
            { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2023, 3, 23), Rate = 1.21m };

        var actualValue =
            _exchangeRateRepository.GetExchangeRate(IsoCurrency.GbpCurrencyCode, IsoCurrency.EurCurrencyCode,
                new DateTime(2023, 3, 23));

        actualValue.Should().BeEquivalentTo(expectedValue);
    }

    [Fact]
    public void ExchangeRateAccess_GbpToNotARealCurrencyCode_ThrowsError()
    {
        const string currencyCodeFrom = IsoCurrency.GbpCurrencyCode;
        const string currencyCodeTo = "NotARealCurrencyCode";

        _exchangeRateRepository.Invoking(x => x.GetExchangeRate(currencyCodeFrom, currencyCodeTo))
            .Should().Throw<Exception>()
            .WithMessage($"Exchange rate not found for {currencyCodeFrom} to {currencyCodeTo} at date {DateTime.Now}");
    }
}
