using System;
using System.Linq.Expressions;
using Common.Http.Authentication;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Common.Http.Tests.TestFramework;
using Xunit;

namespace Common.Http.Tests.Authentication;

public class BearerAudienceAuthenticationContextTests
{
    private static readonly Faker Faker = new();
    
    [Fact]
    public async Task AcquireTokenAsync_AcquiresToken()
    {
        // Arrange
        var clientName = Faker.Random.AlphaNumeric(10);
        var authenticationApiUrl = $"https://{Faker.Internet.DomainName()}/";

        var apiConfiguration = new TestBearerScopeApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            ClientKey = Faker.Random.AlphaNumeric(10),
            ClientSecret = Faker.Random.AlphaNumeric(10),
            Scope = Faker.Random.AlphaNumeric(10)
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.OK, clientName, message => message.RequestUri.ToString() == authenticationApiUrl);

        var target = new BearerAudienceAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
        var cancellationToken = new CancellationToken();

        // Act
        var result = await target.AcquireTokenAsync(cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be("accessToken");
    }

    [Fact]
    public async Task AcquireTokenAsync_RefusesInvalidEndpoint()
    {
        // Arrange
        var clientName = Faker.Random.AlphaNumeric(10);

        var apiConfiguration = new TestBearerScopeApiConfiguration
        {
            AuthenticationApiUrl = "InvalidUrl",
            AuthenticationClientName = clientName,
            ClientKey = Faker.Random.AlphaNumeric(10),
            ClientSecret = Faker.Random.AlphaNumeric(10),
            Scope = Faker.Random.AlphaNumeric(10)
        };

        var target = new BearerAudienceAuthenticationContext<TestAuthenticationResult>(apiConfiguration, Mock.Of<IHttpClientFactory>());
        var cancellationToken = new CancellationToken();

        // Act
        Func<Task> act = async () => await target.AcquireTokenAsync(cancellationToken);

        // Assert
        await Assert.ThrowsAsync<UriFormatException>(act);
    }

    [Fact]
    public async Task AcquireTokenAsync_WhenResultIsNotSuccess_ThrowsError()
    {
        // Arrange
        var clientName = Faker.Random.AlphaNumeric(10);
        var authenticationApiUrl = $"https://{Faker.Internet.DomainName()}/";

        var apiConfiguration = new TestBearerScopeApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            ClientKey = Faker.Random.AlphaNumeric(10),
            ClientSecret = Faker.Random.AlphaNumeric(10),
            Scope = Faker.Random.AlphaNumeric(10)
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.InternalServerError, clientName, message => message.RequestUri.ToString() == authenticationApiUrl);

        var target = new BearerAudienceAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
        var cancellationToken = new CancellationToken();

        // Act
        Func<Task> act = async () => await target.AcquireTokenAsync(cancellationToken);

        // Assert
        await Assert.ThrowsAsync<HttpRequestException>(act);
    }

    private IHttpClientFactory SetupClientFactory(HttpStatusCode statusCode, string clientName, Expression<Func<HttpRequestMessage, bool>> requestMessageExpression)
    {
        var jsonContent = JsonConvert.SerializeObject(new TestAuthenticationResult
        {
            AccessToken = "accessToken"
        });
        var httpClient = HttpClientMock.Create(statusCode, jsonContent, requestMessageExpression).HttpClient;

        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock
            .Setup(m => m.CreateClient(clientName))
            .Returns(httpClient);
        return factoryMock.Object;
    }
}