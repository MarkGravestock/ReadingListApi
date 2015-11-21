using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Raven.Client.Document;

namespace ReadingList.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var documentStore = UseInstalledRavenDocumentStore();
            documentStore.Initialize();

            new ServiceConfigurer(documentStore).ConfigureServices(services);
        }

        private static DocumentStore UseInstalledRavenDocumentStore()
        {
            return new DocumentStore {Url = "http://localhost:8080", DefaultDatabase = "ReadingList"};
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddConsole();

            ConfigureApplication(app);
        }

        public void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseMvc();

            app.UseRuntimeInfoPage();
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
        }
    }
}
