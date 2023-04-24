using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Common.Http.Extensions;

public static class HttpResponseMessageExtension
{
    public static Task<TResponse> ReadHttpResponseAsync<TResponse>(this HttpResponseMessage httpResponse)
    {
        return httpResponse.ReadHttpResponseAsync<TResponse>(new JsonSerializerSettings());
    }

    public static async Task<TResponse?> ReadHttpResponseAsync<TResponse>(this HttpResponseMessage httpResponse, JsonSerializerSettings jsonSerializerSettings)
    {
        httpResponse.EnsureSuccessStatusCode();

        await using var stream = await httpResponse.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(reader);
        var serializer = JsonSerializer.Create(jsonSerializerSettings);

        return serializer.Deserialize<TResponse>(jsonReader);
    }
}
