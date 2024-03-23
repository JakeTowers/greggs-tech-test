using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly ICurrencyConverterService _currencyConverterService;
    private readonly IDataAccess<Product> _productRepository;

    public ProductService(ILogger<ProductService> logger, ICurrencyConverterService currencyConverterService,
        IDataAccess<Product> productRepository)
    {
        _logger = logger;
        _currencyConverterService = currencyConverterService;
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetProducts(int? pageStart, int? pageSize,
        string isoCurrencyCode = IsoCurrency.GbpCurrencyCode)
    {
        var products = _productRepository.List(pageStart, pageSize);

        return isoCurrencyCode switch
        {
            IsoCurrency.GbpCurrencyCode => products,
            IsoCurrency.EurCurrencyCode => products.Select(product => new Product
            {
                Name = product.Name,
                PriceInPounds = product.PriceInPounds,
                PriceInEuros = _currencyConverterService.ConvertCurrency(product.PriceInPounds,
                    IsoCurrency.GbpCurrencyCode, IsoCurrency.EurCurrencyCode)
            }),
            _ => throw new Exception($"ISO Currency Code {isoCurrencyCode} is not supported")
        };
    }
}
