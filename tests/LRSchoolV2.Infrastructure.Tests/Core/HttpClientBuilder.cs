using Moq;
using Moq.Contrib.HttpClient;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
namespace LRSchoolV2.Infrastructure.Tests.Core;

public static class HttpClientBuilder
{
    public static HttpClient CreateTestClient(this Mock<HttpMessageHandler> inHandler)
    {
        var httpClient = inHandler.CreateClient();
        httpClient.BaseAddress = new Uri("https://test.com");
        return httpClient;
    }
}