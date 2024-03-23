using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Services;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IDataAccess<Product> _productRepository;

    public ProductService(ILogger<ProductService> logger, IDataAccess<Product> productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public IEnumerable<Product> GetProducts(int? pageStart, int? pageSize)
    {
        return _productRepository.List(pageStart, pageSize);
    }
}
