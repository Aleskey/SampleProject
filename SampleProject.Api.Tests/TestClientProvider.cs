using System;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace SampleProject.Api.Tests
{
    public class TestClientProvider : IDisposable
    {
        private TestServer server;

        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder().UseEnvironment("Testing").UseStartup<TestStartup>());
            Client = server.CreateClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }

        ~TestClientProvider()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                server?.Dispose();
                server = null;

                Client?.Dispose();
                Client = null;
            }
        }
    }
}