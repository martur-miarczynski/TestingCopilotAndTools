using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Http;

public class HttpServiceOptions
{
    public bool CacheEmptyResponse { get; set; }
    public IDictionary<string, string> QueryParams { get; } = new Dictionary<string, string?>();
    public JsonSerializerSettings JsonSerializerSettings { get; set; } = new();
}