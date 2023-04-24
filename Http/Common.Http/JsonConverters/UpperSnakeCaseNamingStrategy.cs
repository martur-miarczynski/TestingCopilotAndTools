using Newtonsoft.Json.Serialization;
using System.Globalization;

namespace Common.Http.JsonConverters;

public class UpperSnakeCaseNamingStrategy : SnakeCaseNamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return base.ResolvePropertyName(name).ToUpper(CultureInfo.InvariantCulture);
    }
}