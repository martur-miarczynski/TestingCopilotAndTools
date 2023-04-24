namespace Common.Http.Entities;

public class OrderLine
{
    public string OrderLineNumber { get; set; }
    public string OrderLineDescription { get; set; }
    public decimal OrderLineAmount { get; set; }
    public string OrderLineCurrency { get; set; }
    public string OrderLineStatus { get; set; }
    public string OrderLineType { get; set; }
    public Product Product { get; set; }
}