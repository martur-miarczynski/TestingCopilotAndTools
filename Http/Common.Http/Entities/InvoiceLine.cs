using System.Collections.Generic;

namespace Common.Http.Entities;

public class InvoiceLine
{
    public string InvoiceLineNumber { get; set; }
    public string InvoiceLineDescription { get; set; }
    public decimal InvoiceLineAmount { get; set; }
    public string InvoiceLineCurrency { get; set; }
    public string InvoiceLineStatus { get; set; }
    public string InvoiceLineType { get; set; }
    public List<Product> Products { get; set; }
}