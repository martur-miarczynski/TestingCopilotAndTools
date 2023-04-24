using System;
using System.Collections.Generic;
using Bogus;
using FizzWare.NBuilder;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Http.Extensions;
using Common.Http.Tests.TestFramework;
using FluentAssertions;
using Xunit;

namespace Common.Http.Tests.Extensions;

public class HttpClientExtensionTests
{
    private readonly Builder _builder;
    private readonly Faker _faker;

    public HttpClientExtensionTests()
    {
        _builder = new Builder();
        _faker = new Faker();
    }

    [Fact]
    public async Task PostAsync_ReturnsHttpResponseMessage()
    {
        var endPoint = _faker.Internet.Url();
        var request = _builder.CreateNew<object>().Build();
        var expectedResponse = new List<string>
        {
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10),
            _faker.Random.AlphaNumeric(10)
        };

        var (httpClient, _) = HttpClientMock.Create(HttpStatusCode.OK,
            JsonConvert.SerializeObject(expectedResponse),
            requestMessage => requestMessage.RequestUri == new Uri(endPoint));
        var responseContent = JsonConvert.SerializeObject(expectedResponse);

        // Act
        var actualResponse = await httpClient.PostAsync<object>(request, endPoint, CancellationToken.None);

        // Assert
        actualResponse.Should().NotBeNull();

        var actualContent = await actualResponse.Content.ReadAsStringAsync();
        actualContent.Should().Be(responseContent);
    }
}
