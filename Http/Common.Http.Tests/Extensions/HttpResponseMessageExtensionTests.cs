using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Common.Http.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Common.Http.Tests.Extensions;

public class HttpResponseMessageExtensionTests
{
    private readonly Faker _faker;

    public HttpResponseMessageExtensionTests()
    {
        _faker = new Faker();
    }

    [Fact]
    public async Task ReadHttpResponseAsync_ReadsContentAsStream_ReturnsDeserializedResponse()
    {
        // Arrange
        var expectedResponse = new List<string>
        {
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10)
        };

        var responseStringContent = new StringContent(JsonConvert.SerializeObject(expectedResponse), Encoding.UTF8,
            MediaTypeNames.Application.Json);
        var responseMessage = new HttpResponseMessage
        { StatusCode = HttpStatusCode.OK, Content = responseStringContent };

        // Act
        var actualResponse = await responseMessage.ReadHttpResponseAsync<List<string>>();

        // Assert
        expectedResponse.Should().BeEquivalentTo(actualResponse);
    }
}
