using Bogus;
using Common.Http.DelegatingHandlers;
using Common.Http.Interfaces.Authentication;
using Common.Http.Tests.TestFramework;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Common.Http.Tests.DelegatingHandlers;

public class BearerTokenDelegatingHandlerTests
{
    private readonly MockRepository _mockRepository;
    private readonly Mock<IAuthenticationContext<TestAuthenticationResult>> _mockAuthenticationContext;
    private readonly Faker _faker = new();
    private readonly Builder _builder = new();

    public BearerTokenDelegatingHandlerTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _mockAuthenticationContext = _mockRepository.Create<IAuthenticationContext<TestAuthenticationResult>>();
    }

    [Fact]
    public async Task SendAsync_SendMessageWithAuthorizationHeader()
    {
        // Arrange
        var expectedAccessToken = _faker.Random.AlphaNumeric(10);
        var expectedAuthenticationResult = new TestAuthenticationResult
        {
            AccessToken = expectedAccessToken
        };

        var expectedResponsePayload = _builder.CreateNew<TestResponse>().Build();
        var (_, httpClientHandlerMock) = HttpClientMock.Create(HttpStatusCode.OK, JsonConvert.SerializeObject(expectedResponsePayload));

        var cancellationToken = new CancellationToken();
        var testApiConfiguration = _builder.CreateNew<TestClientCredentialApiConfiguration>().Build();

        _mockAuthenticationContext.Setup(x => x.AcquireTokenAsync(cancellationToken))
            .ReturnsAsync(expectedAuthenticationResult);

        var bearerTokenDelegatingHandler = new TestDelegatingHandler<TestClientCredentialApiConfiguration, TestAuthenticationResult>(
            _mockAuthenticationContext.Object,
            testApiConfiguration
        )
        {
            InnerHandler = httpClientHandlerMock.Object
        };

        // Act
        var result = await bearerTokenDelegatingHandler.SendAsync(new HttpRequestMessage(), cancellationToken);

        // Assert
        _mockRepository.VerifyAll();
        result.IsSuccessStatusCode.Should().BeTrue();
        var rawContent = await result.Content.ReadAsStringAsync(cancellationToken);
        var responseContent = JsonConvert.DeserializeObject<TestResponse>(rawContent);
        responseContent.Should().BeEquivalentTo(expectedResponsePayload);
    }

    public class TestDelegatingHandler<TApiConfiguration, TAuthenticationResult> : BearerTokenDelegatingHandler<TApiConfiguration, TAuthenticationResult>
        where TApiConfiguration : IClientCredentialAuthenticationConfiguration
        where TAuthenticationResult : IAuthenticationResult
    {
        public TestDelegatingHandler(IAuthenticationContext<TAuthenticationResult> authenticationContext, TApiConfiguration apiConfiguration) : base(authenticationContext, apiConfiguration)
        {
        }

        public new async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }

    public class TestResponse
    {
        public string TestProperty { get; set; }
    }
}