using System.Collections.Generic;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.Configuration;
using Raven.Client;

namespace ReadingList.Api.AcceptanceTests
{
    public class TestWebHostBuilder
    {
        private string uri;
        private IDocumentStore documentStore;

        public TestWebHostBuilder UsingUri(string uri)
        {
            this.uri = uri;
            return this;
        }

        public TestWebHostBuilder UsingDocumentStore(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
            return this;
        }

        public IHostingEngine Build()
        {
            var configBuider = new ConfigurationBuilder();

            configBuider.AddInMemoryCollection(new Dictionary<string, string> {{"server.urls", uri}});
           
            var builder = new WebHostBuilder(CallContextServiceLocator.Locator.ServiceProvider, configBuider.Build());
           
            builder.UseStartup(app => { new Startup().ConfigureApplication(app); }, services => { new ServiceConfigurer(documentStore).ConfigureServices(services); });
            builder.UseServer("Microsoft.AspNet.Server.Kestrel");

            return builder.Build();
        }
    }
}