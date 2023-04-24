using System;
using Common.Http.Entities;
using Common.Http.Interfaces.Csv;
using Common.Http.Services;
using Moq;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Xunit;

namespace Common.Http.Tests.Services;

public class ProductCsvCreatorTests
{
    private readonly Mock<IGenericCsvFileBuilder<Product>> _productCsvFileBuilderMock;
    private readonly Builder _builder = new();

    public ProductCsvCreatorTests()
    {
        _productCsvFileBuilderMock = new Mock<IGenericCsvFileBuilder<Product>>();
    }

    [Fact]
    public void CreateCsvFile_WhenCalledWithNullProducts_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = new ProductCsvCreator(_productCsvFileBuilderMock.Object);

        // Act
        var exception = Record.Exception(() => sut.CreateCsvFile(null));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void CreateCsvFile_WhenCalledWithEmptyProducts_ShouldThrowArgumentException()
    {
        // Arrange
        var products = new List<Product>();
        var sut = new ProductCsvCreator(_productCsvFileBuilderMock.Object);
        
        // Act
        var exception = Record.Exception(() => sut.CreateCsvFile(products));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<ArgumentException>(exception);
    }

    [Fact]
    public void CreateCsvFile_WhenCalled_ShouldAddHeaders()
    {
        // Arrange
        var products = _builder.CreateListOfSize<Product>(10).Build();
        var sut = new ProductCsvCreator(_productCsvFileBuilderMock.Object);

        // Act
        sut.CreateCsvFile(products);
        // Assert
        _productCsvFileBuilderMock.Verify(x => x.AddHeaders(), Times.Once);
    }

    [Fact]
    public void CreateCsvFile_WhenCalled_ShouldAddLines()
    {
        // Arrange
        var products = new List<Product>
        {
            new()
            {
                ProductNumber = "123",
                ProductName = "Test",
                ProductDescription = "Test",
                ProductPrice = 1,
                ProductCurrency = "EUR",
                ProductStatus = "Active",
                ProductType = "Test"
            }
        };
        var sut = new ProductCsvCreator(_productCsvFileBuilderMock.Object);
        
        // Act
        sut.CreateCsvFile(products);

        // Assert
        _productCsvFileBuilderMock.Verify(x => x.AddLine(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void CreateCsvFile_WhenCalled_ShouldBuildFile()
    {
        // Arrange
        var products = _builder.CreateListOfSize<Product>(10).Build();
        var sut = new ProductCsvCreator(_productCsvFileBuilderMock.Object);

        // Act
        sut.CreateCsvFile(products);

        // Assert
        _productCsvFileBuilderMock.Verify(x => x.BuildFile(), Times.Once);
    }
}