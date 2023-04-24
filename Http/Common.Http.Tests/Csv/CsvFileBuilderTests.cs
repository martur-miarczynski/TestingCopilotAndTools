using System;
using System.Text;
using Common.Http.Csv;
using Xunit;

namespace Common.Http.Tests.Csv;

public class CsvFileBuilderTests
{
    [Fact]
    public void AddHeaders_AddsHeadersToBuilder()
    {
        // Arrange
        var builder = new CsvFileBuilder();
        var headers = new[] { "Name", "Age", "Email" };

        // Act
        builder.AddHeaders(headers);

        // Assert
        var expectedContent = $"Name,Age,Email{Environment.NewLine}";
        Assert.Equal(expectedContent, builder.ToString());
    }

    [Fact]
    public void AddLine_AddsLineToBuilder()
    {
        // Arrange
        var builder = new CsvFileBuilder();
        var line = new[] { "John Doe", "30", "john.doe@example.com" };

        // Act
        builder.AddLine(line);

        // Assert
        var expectedContent = $"John Doe,30,john.doe@example.com{Environment.NewLine}";
        Assert.Equal(expectedContent, builder.ToString());
    }

    [Fact]
    public void BuildFile_ReturnsUtf8EncodedByteArray()
    {
        // Arrange
        var builder = new CsvFileBuilder();
        var headers = new[] { "Name", "Age", "Email" };
        var line1 = new[] { "John Doe", "30", "john.doe@example.com" };
        var line2 = new[] { "Jane Smith", "25", "jane.smith@example.com" };
        builder.AddHeaders(headers);
        builder.AddLine(line1);
        builder.AddLine(line2);

        // Act
        var fileContent = builder.BuildFile();

        // Assert
        // głupota, builder nie ma ToString
        var expectedContent = Encoding.UTF8.GetBytes(builder.ToString());
        Assert.Equal(expectedContent, fileContent);
    }
}