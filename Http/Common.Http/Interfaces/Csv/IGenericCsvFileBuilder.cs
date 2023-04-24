using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Http.Interfaces.Csv;

public interface ICsvFileBuilder
{
    void AddHeaders(IEnumerable<string> headers);
    void AddLine(IEnumerable<string> line);
    byte[] BuildFile();
}

public interface IGenericCsvFileBuilder<TModel> where TModel : class
{
    void AddHeaders();
    void AddLine(TModel model);
    byte[] BuildFile();
}