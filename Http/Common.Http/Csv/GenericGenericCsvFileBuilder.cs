using Common.Http.Interfaces.Csv;
using System.Linq;

namespace Common.Http.Csv;

public class GenericGenericCsvFileBuilder<TModel> : IGenericCsvFileBuilder<TModel> where TModel : class
{
    private readonly CsvFileBuilder _csvFileBuilder;

    public GenericGenericCsvFileBuilder(CsvFileBuilder csvFileBuilder)
    {
        _csvFileBuilder = csvFileBuilder;
    }

    public void AddHeaders()
    {
        _csvFileBuilder.AddHeaders(GetPropertiesNamesOrderedByNameAscending());
    }

    public void AddLine(TModel model)
    {
        _csvFileBuilder.AddLine(GetPropertiesValuesOrderedByNameAscending());
    }

    public byte[] BuildFile()
    {
        return _csvFileBuilder.BuildFile();
    }

    private string[] GetPropertiesNamesOrderedByNameAscending()
    {
        return typeof(TModel).GetProperties().Select(x => x.Name).OrderBy(x => x).ToArray();
    }

    private string[] GetPropertiesValuesOrderedByNameAscending()
    {
        return typeof(TModel).GetProperties().Select(x => x.GetValue(x)?.ToString()).OrderBy(x => x).ToArray();
    }
}