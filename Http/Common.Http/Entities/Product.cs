namespace Common.Http.Entities;

public class Product
{
    public string ProductNumber { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductCurrency { get; set; }
    public string ProductStatus { get; set; }
    public string ProductType { get; set; }
}