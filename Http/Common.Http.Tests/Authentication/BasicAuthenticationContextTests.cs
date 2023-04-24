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
using Common.Http.Exception;
using Common.Http.Tests.TestFramework;
using Xunit;

namespace Common.Http.Tests.Authentication;

public class BasicAuthenticationContextTests
{
    [Fact]
    public async Task AcquireTokenAsync_AcquiresToken()
    {
        // Arrange
        var clientName = "authenticationClientName";
        var authenticationApiUrl = "https://testurl.maersk.com/";

        var apiConfiguration = new TestBasicApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            Password = "password",
            UserName = "userName"
        };
        var encodedCredentials = "dXNlck5hbWU6cGFzc3dvcmQ=";

        var clientFactory = SetupClientFactory(HttpStatusCode.OK, clientName, message => VerifyAuthenticationRequest(message, authenticationApiUrl, encodedCredentials));

        var target = new BasicAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
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

        var apiConfiguration = new TestBasicApiConfiguration
        {
            AuthenticationApiUrl = "InvalidUrl",
            AuthenticationClientName = clientName,
            Password = "password",
            UserName = "userName"
        };

        var target = new BasicAuthenticationContext<TestAuthenticationResult>(apiConfiguration, Mock.Of<IHttpClientFactory>());
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

        var apiConfiguration = new TestBasicApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            Password = "password",
            UserName = "userName"
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.InternalServerError, clientName, message => message.RequestUri.ToString() == authenticationApiUrl);

        var target = new BasicAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
        var cancellationToken = new CancellationToken();

        // Act
        Func<Task> act = async () => await target.AcquireTokenAsync(cancellationToken);

        // Assert
        await Assert.ThrowsAsync<HttpRequestException>(act);
    }

    [Fact]
    public async Task AcquireTokenAsync_WhenResultIsNull_ThrowsError()
    {
        // Arrange
        var clientName = "authenticationClientName";
        var authenticationApiUrl = "https://testurl.maersk.com/";

        var apiConfiguration = new TestBasicApiConfiguration
        {
            AuthenticationApiUrl = authenticationApiUrl,
            AuthenticationClientName = clientName,
            Password = "password",
            UserName = "userName"
        };

        var clientFactory = SetupClientFactory(HttpStatusCode.OK, clientName, 
            message => message.RequestUri.ToString() == authenticationApiUrl, invalidResponse: true);

        var target = new BasicAuthenticationContext<TestAuthenticationResult>(apiConfiguration, clientFactory);
        var cancellationToken = new CancellationToken();

        // Act
        Func<Task> act = async () => await target.AcquireTokenAsync(cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ApiAuthenticationException>(act);
    }

    private bool VerifyAuthenticationRequest(HttpRequestMessage httpRequestMessage, string authenticationApiUrl, string encodedCredentials)
    {
        return httpRequestMessage!.RequestUri!.ToString() == authenticationApiUrl
               && httpRequestMessage!.Headers!.Authorization!.Scheme == "Basic"
               && httpRequestMessage.Headers.Authorization.Parameter == encodedCredentials;
    }

    private IHttpClientFactory SetupClientFactory(HttpStatusCode statusCode, string clientName, Expression<Func<HttpRequestMessage, bool>> requestMessageExpression, bool invalidResponse = false)
    {
        var jsonContent = invalidResponse ? " " : JsonConvert.SerializeObject(new TestAuthenticationResult
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