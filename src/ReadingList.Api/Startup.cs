﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Raven.Client.Document;

namespace ReadingList.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var documentStore = new DocumentStore {Url = "http://localhost:8080", DefaultDatabase = "ReadingList"};
            documentStore.Initialize();

            new ServiceConfigurer(documentStore).ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Debug;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            ConfigureApplication(app);
        }

        public void ConfigureApplication(IApplicationBuilder app)
        {
            app.UseMvc();

            app.UseRuntimeInfoPage();
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
        }
    }
}