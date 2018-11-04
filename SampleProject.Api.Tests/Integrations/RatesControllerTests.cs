using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace SampleProject.Api.Tests.Integrations
{
    public class RatesControllerTests
    {
        [Fact]
        public void RetrieveRatesForGivenDatesTest()
        {
            // Arrange
            var requestUrisWithResult = new Dictionary<string, string>
            {
                { "/api/rates/2018-10-01T10:00:00Z/2018-10-01T20:00:00Z", "{\"price\":1500.0}"},
                { "/api/rates/2018-10-05T09:00:00Z/2018-10-05T21:00:00Z", "{\"price\":2000.0}"},
                { "/api/rates/2018-10-03T06:00:00Z/2018-10-03T18:00:00Z", "{\"price\":1750.0}"},
                { "/api/rates/2018-10-02T01:00:00Z/2018-10-02T07:00:00Z", "{\"price\":925.0}"}
            };
            var client = CreateHttpClient();

            // Act
            var responses = Task.WhenAll(requestUrisWithResult.Select(uri => client.GetAsync(uri.Key))).GetAwaiter().GetResult();

            // Assert
            foreach (var response in responses)
            {
                AssertEqual(response, requestUrisWithResult[response.RequestMessage.RequestUri.LocalPath]);
            }
        }

        [Fact]
        public void RetrieveBadRequestForGivenDatesTest()
        {
            // Arrange
            var requestUris = new[]
            {
                "/api/rates/2018-10-01T12:00:00Z/2018-10-02T05:00:00Z",
                "/api/rates/2018-10-04T00:00:00Z/2018-10-04T00:00:00Z"
            };
            var client = CreateHttpClient();

            // Act
            var responses = Task.WhenAll(requestUris.Select(uri => client.GetAsync(uri))).GetAwaiter().GetResult();

            // Assert
            foreach (var response in responses)
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public void RetrieveNotFoundWhenDateNotFallInAnyRanges()
        {
            // Arrange
            var requestUri = "/api/rates/2018-10-01T06:00:00Z/2018-10-01T08:00:00Z";
            var client = CreateHttpClient();

            // Act
            var response = client.GetAsync(requestUri).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            AssertEqual(response, "\"Unavailable\"");
        }

        private static HttpClient CreateHttpClient()
        {
            var server = new TestServer(WebHost.CreateDefaultBuilder().UseEnvironment("Testing").UseStartup<TestStartup>());
            var client = server.CreateClient();
            return client;
        }

        private static void AssertEqual(HttpResponseMessage message, string expected)
        {
            Assert.NotNull(message);
            Assert.NotNull(expected);

            var result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Assert.Equal(expected, result);

        }
    }
}