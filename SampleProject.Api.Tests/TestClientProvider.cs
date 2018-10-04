using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.DataAccess;

namespace SampleProject.Api.Tests
{
    public class TestClientProvider : IDisposable
    {
        private TestServer server;

        public HttpClient Client { get; }

        public TestClientProvider()
        {
            
            server = new TestServer(WebHost.CreateDefaultBuilder().UseEnvironment("Testing")
                .UseStartup<TestStartup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server?.Dispose();
            Client?.Dispose();
        }
    }
}