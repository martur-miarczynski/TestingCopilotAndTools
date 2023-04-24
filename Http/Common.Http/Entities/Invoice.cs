using System.Collections.Generic;
using System.Linq;

namespace Common.Http.Entities;

public class Invoice
{
    public string InvoiceNumber { get; set; }
    public string InvoiceDate { get; set; }
    public decimal InvoiceAmount { get; set; }
    public string InvoiceCurrency { get; set; }
    public string InvoiceStatus { get; set; }
    public string InvoiceType { get; set; }
    public string InvoiceDueDate { get; set; }
    public decimal InvoiceDueAmount { get; set; }
    public string InvoiceDueCurrency { get; set; }
    public string InvoiceDueStatus { get; set; }
    public string InvoiceDueType { get; set; }

    public List<InvoiceLine> InvoiceLines { get; set; }

    public decimal GetTotalFromInvoiceLines()
    {
        return InvoiceLines.Sum(x => x.InvoiceLineAmount);
    }

    public decimal GetAverageFromInvoiceLines()
    {
        return InvoiceLines.Average(x => x.InvoiceLineAmount);
    }

    public string GetProductWithHighestPrice()
    {
        return InvoiceLines.SelectMany(x => x.Products)
            .OrderByDescending(x => x.ProductPrice).First().ProductName;
    }
}