using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.Configuration;
using Xunit;

namespace ReadingListApi.AcceptanceTests
{
    public class ReadingListJsonTests
    {
        [Fact]
        public void ReturnsExpectedHttpResult()
        {
            var uri = "http://localhost:5123";
            var baseAddress = new Uri(uri);
            var configBuider = new ConfigurationBuilder();

            configBuider.AddInMemoryCollection(new Dictionary<string, string> {{"server.urls", uri}});
            var builder = new WebHostBuilder(CallContextServiceLocator.Locator.ServiceProvider, configBuider.Build());
            builder.UseStartup<Startup>();
            builder.UseServer("Microsoft.AspNet.Server.Kestrel");

            var host = builder.Build();
            using (var app = host.Start())
            {
                var client = new HttpClient {BaseAddress = baseAddress};

                var response = client.GetAsync("api/ReadingList").Result;

                Assert.True(response.IsSuccessStatusCode, "Actual status code: " + response.StatusCode);
            }
        }
    }
}