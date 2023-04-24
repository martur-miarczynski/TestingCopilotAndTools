using Common.Http.Interfaces.Csv;
using System.Collections.Generic;
using System.Text;

namespace Common.Http.Csv;

public class CsvFileBuilder : ICsvFileBuilder
{
    private readonly StringBuilder _fileContentBuilder = new();
    public const string Separator = ",";

    public void AddHeaders(IEnumerable<string> headers)
    {
        _fileContentBuilder.AppendLine(string.Join(Separator, headers));
    }

    public void AddLine(IEnumerable<string> line)
    {
        _fileContentBuilder.AppendLine(string.Join(Separator, line));
    }

    public byte[] BuildFile()
    {
        return Encoding.UTF8.GetBytes(_fileContentBuilder.ToString());
    }
}