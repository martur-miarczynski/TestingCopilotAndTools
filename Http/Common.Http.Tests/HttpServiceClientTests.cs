using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Common.Http.Exception;
using Common.Http.Tests.TestFramework;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Common.Http.Tests
{
    public class HttpServiceClientTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly MockRepository _mockRepository;
        private readonly Faker _faker;

        public HttpServiceClientTests()
        {
            _faker = new();
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _httpClientFactoryMock = _mockRepository.Create<IHttpClientFactory>();
        }

        [Fact]
        public async Task GetAsync_WhenResponseNull_Throws_EmptyResponseException()
        {
            // Arrange
            var clientName = _faker.Random.AlphaNumeric(10);
            var expectedUrl = _faker.Internet.Url();
            var (httpClient, _) = HttpClientMock.Create(HttpStatusCode.OK,
                string.Empty,
                requestMessage => requestMessage.RequestUri == new Uri(expectedUrl));
            _httpClientFactoryMock.Setup(x => x.CreateClient(clientName)).Returns(httpClient);
            var client = new HttpServiceClient(_httpClientFactoryMock.Object);

            // Act
            var act = () => client.GetAsync<List<string>>(clientName, expectedUrl, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EmptyResponseException>();
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_WhenResponse_EnsureSuccessStatusCodeIsFalse_Throws_HttpRequestException()
        {
            // Arrange
            var clientName = _faker.Random.AlphaNumeric(10);
            var expectedUrl = _faker.Internet.Url();
            var (httpClient, _) = HttpClientMock.Create(HttpStatusCode.BadRequest,
                string.Empty,
                requestMessage => requestMessage.RequestUri == new Uri(expectedUrl));
            _httpClientFactoryMock.Setup(x => x.CreateClient(clientName)).Returns(httpClient);
            var client = new HttpServiceClient(_httpClientFactoryMock.Object);

            // Act
            var act = () => client.GetAsync<List<string>>(clientName, expectedUrl, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>();
            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAsync_ReturnsHttpResponse()
        {
            // Arrange
            var expectedResponse = CreateExpectedResponse();
            var clientName = _faker.Random.AlphaNumeric(10);
            var expectedUrl = _faker.Internet.Url();
            var (httpClient, _) = HttpClientMock.Create(HttpStatusCode.OK,
                JsonConvert.SerializeObject(expectedResponse),
                requestMessage => requestMessage.RequestUri == new Uri(expectedUrl));
            _httpClientFactoryMock.Setup(x => x.CreateClient(clientName)).Returns(httpClient);
            var client = new HttpServiceClient(_httpClientFactoryMock.Object);

            // Act
            var result = await client.GetAsync<List<string>>(clientName, expectedUrl, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
            _mockRepository.VerifyAll();
        }

        private List<string> CreateExpectedResponse()
        {
            return new List<string>
            {
                _faker.Random.AlphaNumeric(10),
                _faker.Random.AlphaNumeric(10),
                _faker.Random.AlphaNumeric(10),
                _faker.Random.AlphaNumeric(10)
            };
        }
    }
}
