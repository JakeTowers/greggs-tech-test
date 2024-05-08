using System;
using System.Collections.Generic;
using FluentAssertions;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductServiceTests
{
    private readonly ProductService _productService;

    private static readonly IEnumerable<Product> ProductDatabase = new List<Product>()
    {
        new() { Name = "Sausage Roll", PriceInPounds = 1m },
        new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m },
        new() { Name = "Steak Bake", PriceInPounds = 1.2m },
        new() { Name = "Yum Yum", PriceInPounds = 0.7m },
        new() { Name = "Pink Jammie", PriceInPounds = 0.5m },
        new() { Name = "Mexican Baguette", PriceInPounds = 2.1m },
        new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m },
        new() { Name = "Coca Cola", PriceInPounds = 1.2m }
    };

    private static readonly ExchangeRate ExchangeRate = new()
        { From = IsoCurrency.Gbp, To = IsoCurrency.Eur, Date = new DateTime(2024, 3, 23), Rate = 1.11m };


    public ProductServiceTests()
    {
        var productRepositoryMock = new Mock<IDataAccess<Product>>();
        productRepositoryMock.Setup(x =>
                x.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(ProductDatabase);

        var exchangeRateRepositoryMock = new Mock<IExchangeRateAccess>();
        exchangeRateRepositoryMock.Setup(x =>
                x.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
            .Returns(ExchangeRate);

        var currencyConverterService = new CurrencyConverterService(
            new Mock<ILogger<CurrencyConverterService>>().Object,
            exchangeRateRepositoryMock.Object);

        _productService = new ProductService(new Mock<ILogger<ProductService>>().Object,
            currencyConverterService, productRepositoryMock.Object);
    }

    [Fact]
    public void GetProducts_AllProducts_InPounds()
    {
        const int pageStart = 0;
        const int pageSize = 8;
        var expectedProducts = new List<Product>
        {
            new() { Name = "Sausage Roll", PriceInPounds = 1m },
            new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m },
            new() { Name = "Steak Bake", PriceInPounds = 1.2m },
            new() { Name = "Yum Yum", PriceInPounds = 0.7m },
            new() { Name = "Pink Jammie", PriceInPounds = 0.5m },
            new() { Name = "Mexican Baguette", PriceInPounds = 2.1m },
            new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m },
            new() { Name = "Coca Cola", PriceInPounds = 1.2m }
        };

        var products = _productService.GetProducts(pageStart, pageSize);

        products.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public void GetProducts_AllProducts_InEuros()
    {
        const int pageStart = 0;
        const int pageSize = 8;
        var expectedProducts = new List<Product>
        {
            new() { Name = "Sausage Roll", PriceInPounds = 1m, PriceInEuros = 1.11m },
            new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m, PriceInEuros = 1.221m },
            new() { Name = "Steak Bake", PriceInPounds = 1.2m, PriceInEuros = 1.332m },
            new() { Name = "Yum Yum", PriceInPounds = 0.7m, PriceInEuros = 0.777m },
            new() { Name = "Pink Jammie", PriceInPounds = 0.5m, PriceInEuros = 0.555m },
            new() { Name = "Mexican Baguette", PriceInPounds = 2.1m, PriceInEuros = 2.331m },
            new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m, PriceInEuros = 2.1645m },
            new() { Name = "Coca Cola", PriceInPounds = 1.2m, PriceInEuros = 1.332m }
        };

        var products = _productService.GetProducts(pageStart, pageSize, IsoCurrency.EurCurrencyCode);

        products.Should().BeEquivalentTo(expectedProducts);
    }
}
