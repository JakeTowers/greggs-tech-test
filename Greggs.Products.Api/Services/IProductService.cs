using System.Collections.Generic;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services;

public interface IProductService
{
    IEnumerable<Product> GetProducts(int? pageStart, int? pageSize,
        string isoCurrencyCode = IsoCurrency.GbpCurrencyCode);
}
