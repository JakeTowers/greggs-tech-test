using System;
using System.Collections.Generic;
using Greggs.Products.Api.Models;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductServiceTests
{
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

        throw new NotImplementedException();
    }
}
