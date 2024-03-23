using System;
using Xunit;

namespace Greggs.Products.UnitTests;

public class CurrencyConverterTests
{
    [Fact]
    public void CurrencyConverter_1Pound_ToEurosAtExchangeRate()
    {
        const decimal priceInPounds = 1m;
        const decimal gbpToEurExchangeRate = 1.11m;
        const decimal expectedValue = 1.11m;

        throw new NotImplementedException();
    }
}
