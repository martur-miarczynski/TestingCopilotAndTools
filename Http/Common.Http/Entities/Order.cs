using System.Collections.Generic;

namespace Common.Http.Entities;

public class Order
{
    public string OrderNumber { get; set; }
    public string OrderDate { get; set; }
    public decimal OrderAmount { get; set; }
    public string OrderCurrency { get; set; }
    public string OrderStatus { get; set; }
    public string OrderType { get; set; }
    public List<OrderLine> OrderLines { get; set; }
}