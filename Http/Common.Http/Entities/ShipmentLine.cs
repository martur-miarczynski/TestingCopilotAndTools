namespace Common.Http.Entities;

public class ShipmentLine
{
    public string ShipmentLineNumber { get; set; }
    public string ShipmentLineDescription { get; set; }
    public decimal ShipmentLineAmount { get; set; }
    public string ShipmentLineCurrency { get; set; }
    public string ShipmentLineStatus { get; set; }
    public string ShipmentLineType { get; set; }
    public Product Product { get; set; }
}