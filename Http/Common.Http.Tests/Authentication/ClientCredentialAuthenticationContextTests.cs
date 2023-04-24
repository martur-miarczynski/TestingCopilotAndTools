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
using Common.Http.Tests.TestFramework;
using Xunit;

namespace Common.Http.Tests.Authentication;

public class ClientCredentialAuthenticationContextTests
{
    [Fact]
    public async Task AcquireTokenAsync_AcquiresToken()
    {
        // Arrange
        var clientName = "authenticationClientName";
        var authenticationApiUrl = "https://testurl.maersk.com/";

        var apiConfiguration = new TestClientCredentialApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            ClientKey = "A9464E76-C936-44BB-A418-3FC1E227D6B3",
            ClientSecret = "6ACD3CC2-0D5B-4CA5-AF1F-BD84AE16F537",
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.OK, clientName, message => message.RequestUri.ToString() == authenticationApiUrl);

        var target = new ClientCredentialAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
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
        var clientName = "authenticationClientName";

        var apiConfiguration = new TestClientCredentialApiConfiguration
        {
            AuthenticationApiUrl = "InvalidUrl",
            AuthenticationClientName = clientName,
            ClientKey = "A9464E76-C936-44BB-A418-3FC1E227D6B3",
            ClientSecret = "6ACD3CC2-0D5B-4CA5-AF1F-BD84AE16F537",
        };

        var target = new ClientCredentialAuthenticationContext<TestAuthenticationResult>(apiConfiguration, Mock.Of<IHttpClientFactory>());
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
        var clientName = "authenticationClientName";
        var authenticationApiUrl = "https://testurl.maersk.com/";

        var apiConfiguration = new TestClientCredentialApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            ClientKey = "A9464E76-C936-44BB-A418-3FC1E227D6B3",
            ClientSecret = "6ACD3CC2-0D5B-4CA5-AF1F-BD84AE16F537",
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.InternalServerError, clientName, message => message.RequestUri.ToString() == authenticationApiUrl);

        var target = new ClientCredentialAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
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