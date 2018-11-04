using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleProject.Api.Tests.Integrations
{
    public class RatesControllerTests
    {
        [Fact]
        public void RetrieveRatesForGivenDatesTest()
        {
            // Arrange
            var requestUris = new[]
            {
                "/api/rates/2018-10-01T10:00:00Z/2018-10-01T20:00:00Z",
                "/api/rates/2018-10-05T09:00:00Z/2018-10-05T21:00:00Z",
                "/api/rates/2018-10-03T06:00:00Z/2018-10-03T18:00:00Z",
                "/api/rates/2018-10-02T01:00:00Z/2018-10-02T07:00:00Z"
            };
            var client = new TestClientProvider().Client;

            // Act
            var responses = Task.WhenAll(requestUris.Select(uri => client.GetAsync(uri))).GetAwaiter().GetResult();

            // Assert
            AssertEqual(responses[0], "{\"price\":1500.0}");
            AssertEqual(responses[1], "{\"price\":2000.0}");
            AssertEqual(responses[2], "{\"price\":1750.0}");
            AssertEqual(responses[3], "{\"price\":925.0}");
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
            var client = new TestClientProvider().Client;

            // Act
            var responses = Task.WhenAll(requestUris.Select(uri => client.GetAsync(uri))).GetAwaiter().GetResult();

            // Assert
            foreach (var message in responses)
            {
                Assert.Equal(HttpStatusCode.BadRequest, message.StatusCode);
            }
        }

        [Fact]
        public void RetrieveNotFoundWhenDateNotFallInAnyRanges()
        {
            // Arrange
            var requestUri = "/api/rates/2018-10-01T06:00:00Z/2018-10-01T08:00:00Z";
            var client = new TestClientProvider().Client;

            // Act
            var response = client.GetAsync(requestUri).GetAwaiter().GetResult();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            AssertEqual(response, "\"Unavailable\"");
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