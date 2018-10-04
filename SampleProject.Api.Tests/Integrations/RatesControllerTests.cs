using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using SampleProject.Models;
using Xunit;

namespace SampleProject.Api.Tests.Integrations
{
    public class RatesControllerTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public RatesControllerTests()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();
            server = new TestServer(builder);
            client = server.CreateClient();


        }

        public void Dispose()
        {
            client?.Dispose();
            server?.Dispose();
        }

        public static object[][] CorrectData => new object[][]
        {
            new object[] { new DateTime(2018, 10, 1, 10, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 1, 20, 0, 0, DateTimeKind.Utc), 1500 },
            new object[] { new DateTime(2018, 10, 2, 9, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 2, 20, 0, 0, DateTimeKind.Utc), 1500 },
            new object[] { new DateTime(2018, 10, 4, 11, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 4, 21, 0, 0, DateTimeKind.Utc), 1500 },

            new object[] { new DateTime(2018, 10, 5, 9, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 5, 21, 0, 0, DateTimeKind.Utc), 2000 },
            new object[] { new DateTime(2018, 10, 6, 10, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 6, 20, 0, 0, DateTimeKind.Utc), 2000 },
            new object[] { new DateTime(2018, 10, 7, 11, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 7, 19, 0, 0, DateTimeKind.Utc), 2000 },

            new object[] { new DateTime(2018, 10, 3, 6, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 3, 18, 0, 0, DateTimeKind.Utc), 1750 },

            new object[] { new DateTime(2018, 10, 1, 1, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 1, 2, 0, 0, DateTimeKind.Utc), 1000 },
            new object[] { new DateTime(2018, 10, 3, 2, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 3, 3, 0, 0, DateTimeKind.Utc), 1000 },
            new object[] { new DateTime(2018, 10, 6, 3, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 6, 5, 0, 0, DateTimeKind.Utc), 1000 },

            new object[] { new DateTime(2018, 10, 2, 1, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 2, 7, 0, 0, DateTimeKind.Utc), 925 },
            new object[] { new DateTime(2018, 10, 7, 2, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 7, 6, 0, 0, DateTimeKind.Utc), 925 },
        };

        [Theory]
        [MemberData(nameof(CorrectData))]
        public async Task Get_Positive_Different_Dates_Test(DateTime fromDate, DateTime toDate, decimal price)
        {
            // Arrange
            var fromDateInIso8601 = fromDate.ToString("o");
            var toDateInIso8601 = toDate.ToString("o");

            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.GetAsync($"/api/rates/{fromDateInIso8601}/{toDateInIso8601}");

                response.EnsureSuccessStatusCode();
                var rateModel = await response.Content.ReadAsAsync<RateModel>();

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(price, rateModel.Price, 1);
                Assert.Equal(fromDate.Ticks, rateModel.FromDate.Ticks);
                Assert.Equal(toDate.Ticks, rateModel.ToDate.Ticks);
            }
        }

        public static object[][] BadRequestData => new object[][]
        {
            new object[] { new DateTime(2018, 10, 1, 12, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 2, 5, 0, 0, DateTimeKind.Utc) },
            new object[] { new DateTime(2018, 10, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 2, 1, 0, 0, DateTimeKind.Utc) },
            new object[] { new DateTime(2018, 10, 4, 0, 0, 0, DateTimeKind.Utc), new DateTime(2018, 10, 4, 0, 0, 0, DateTimeKind.Utc) },
        };

        [Theory]
        [MemberData(nameof(BadRequestData))]
        public async Task Get_Incorrect_Parameters_BadRequest_Test(DateTime fromDate, DateTime toDate)
        {
            // Arrange
            var fromDateInIso8601 = fromDate.ToString("o");
            var toDateInIso8601 = toDate.ToString("o");

            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.GetAsync($"/api/rates/{fromDateInIso8601}/{toDateInIso8601}");

                var content = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Equal("\"Passed parameters are not satisfy the requirements.\"", content);
            }
        }

        [Fact]
        public async Task Get_Date_Range_Outside_Existing_NotFound_Test()
        {
            // Arrange
            var fromDateInIso8601 = new DateTime(2018, 10, 1, 6, 0, 0, DateTimeKind.Utc).ToString("o");
            var toDateInIso8601 = new DateTime(2018, 10, 1, 8, 0, 0, DateTimeKind.Utc).ToString("o");

            using (var client = new TestClientProvider().Client)
            {
                // Act
                var response = await client.GetAsync($"/api/rates/{fromDateInIso8601}/{toDateInIso8601}");

                var content = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Equal("\"Unavailable\"", content);
            }
        }
    }
}
