using System.Collections.Generic;

namespace Common.Http.Entities;

public class Shipment
{
    public string ShipmentNumber { get; set; }
    public string ShipmentDate { get; set; }
    public decimal ShipmentAmount { get; set; }
    public string ShipmentCurrency { get; set; }
    public string ShipmentStatus { get; set; }
    public string ShipmentType { get; set; }
    public List<ShipmentLine> ShipmentLines { get; set; }
}