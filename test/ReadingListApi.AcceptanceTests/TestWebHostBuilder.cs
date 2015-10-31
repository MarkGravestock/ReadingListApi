using System;
using System.Collections.Generic;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.Configuration;

namespace ReadingListApi.AcceptanceTests
{
    public class TestWebHostBuilder
    {
        private string uri;

        public TestWebHostBuilder UsingUri(string uri)
        {
            this.uri = uri;
            return this;
        }

        public IHostingEngine Build()
        {
            var baseAddress = new Uri(uri);
            var configBuider = new ConfigurationBuilder();

            configBuider.AddInMemoryCollection(new Dictionary<string, string> {{"server.urls", uri}});
            var builder = new WebHostBuilder(CallContextServiceLocator.Locator.ServiceProvider, configBuider.Build());
            builder.UseStartup<Startup>();
            builder.UseServer("Microsoft.AspNet.Server.Kestrel");

            return builder.Build();
        }
    }
}