using System;

namespace Greggs.Products.Api.Models;

public class ExchangeRate
{
    public Currency From { get; init; }
    public Currency To { get; init; }
    public decimal Rate { get; init; }
    public DateTime Date { get; init; }
}
