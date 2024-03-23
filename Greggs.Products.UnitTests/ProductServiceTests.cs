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

    public ProductServiceTests()
    {
        var productRepositoryMock = new Mock<IDataAccess<Product>>();
        productRepositoryMock.Setup(x =>
                x.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((ProductDatabase));

        _productService = new ProductService(new Mock<ILogger<ProductService>>().Object, productRepositoryMock.Object);
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
}
