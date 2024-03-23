using System;
using FluentAssertions;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests;

public class CurrencyConverterTests
{
    private readonly CurrencyConverterService _currencyConverterService;

    private static readonly ExchangeRate ExchangeRate = new()
        { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2024, 3, 23), Rate = 1.11m };

    public CurrencyConverterTests()
    {
        var exchangeRateRepositoryMock = new Mock<IExchangeRateAccess>();
        exchangeRateRepositoryMock.Setup(x =>
                x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
            .Returns(ExchangeRate);

        _currencyConverterService = new CurrencyConverterService(new Mock<ILogger<CurrencyConverterService>>().Object,
            exchangeRateRepositoryMock.Object);
    }

    [Fact]
    public void CurrencyConverter_1Pound_ToEurosAtExchangeRate()
    {
        const decimal priceInPounds = 1m;
        const decimal expectedValue = 1.11m;

        var actualValue = _currencyConverterService.ConvertCurrency(priceInPounds, IsoCurrency.GbpCurrencyCode,
            IsoCurrency.EurCurrencyCode);

        actualValue.Should().Be(expectedValue);
    }
}
