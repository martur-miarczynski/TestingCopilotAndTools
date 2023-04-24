using System.Collections.Generic;
using System.Linq;

namespace Common.Http.Entities;

public class OrderSearch
{
    private readonly List<Order> _orders;

    public OrderSearch(List<Order> orders, List<Product> products)
    {
        _orders = orders;
    }

    public Product FindMostPopularProductGroupByProductName()
    {
        return _orders.SelectMany(x => x.OrderLines)
            .GroupBy(x => x.Product.ProductName)
            .OrderByDescending(x => x.Count())
            .First().First().Product;
    }

    public List<string> GetDistinctProductName()
    {
        return _orders.SelectMany(x => x.OrderLines)
            .Select(x => x.Product.ProductName)
            .Distinct().ToList();
    }

    public List<Order> GetOrdersWithProduct(string productCode)
    {
        return _orders.Where(x => x.OrderLines.Any(y => y.Product.ProductNumber == productCode)).ToList();
    }
}