using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Common.Http.Tests.TestFramework;

public static class HttpClientMock
{
    private const string SendAsyncMethodName = "SendAsync";

    public static (HttpClient HttpClient, Mock<DelegatingHandler> ClientHandlerMock) Create(HttpStatusCode statusCodeResult, string stringResult = "", Expression<Func<HttpRequestMessage, bool>> requestMessageExpression = null)
    {
        var clientHandlerMock = new Mock<DelegatingHandler>();
        clientHandlerMock.SetupHttpResponse(statusCodeResult, stringResult, requestMessageExpression);
        return (new HttpClient(clientHandlerMock.Object), clientHandlerMock);
    }

    public static void SetupHttpResponse(this Mock<DelegatingHandler> mockMessageHandler, HttpStatusCode statusCodeResult, string stringResult = "", Expression<Func<HttpRequestMessage, bool>> requestMessageExpression = null)
    {
        var expression = requestMessageExpression != null ? ItExpr.Is(requestMessageExpression) : ItExpr.IsAny<HttpRequestMessage>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(SendAsyncMethodName, expression, ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() => new HttpResponseMessage(statusCodeResult)
            {
                Content = new StringContent(stringResult)
            })
            .Verifiable();
        mockMessageHandler.As<IDisposable>().Setup(s => s.Dispose());
    }
}