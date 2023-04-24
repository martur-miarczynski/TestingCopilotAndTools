using System;
using Common.Http.Entities;
using Common.Http.Interfaces.Csv;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Common.Http.Services;

public class ProductCsvCreator
{
    private readonly IGenericCsvFileBuilder<Product> _productCsvFileBuilder;

    public ProductCsvCreator(IGenericCsvFileBuilder<Product> productCsvFileBuilder)
    {
        _productCsvFileBuilder = productCsvFileBuilder;
    }

    public byte[] CreateCsvFile(IEnumerable<Product> products)
    {
        if (products == null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        var productsList = products.ToList();
        if (!productsList.Any())
        {
            throw new ArgumentException("Empty products collection", nameof(products));
        }

        _productCsvFileBuilder.AddHeaders();
        AddLineForEachProduct();
        return _productCsvFileBuilder.BuildFile();

        void AddLineForEachProduct()
        {
            foreach (var product in productsList)
            {
                _productCsvFileBuilder.AddLine(product);
            }
        }
    }
}