using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Ecom.AuthServer.Configuration;

namespace Ecom.AuthServer
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        private IConfigurationRoot Configuration { get; }
        private IHostingEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var certificate = new X509Certificate2(Path.Combine(Environment.WebRootPath, @"Certificate\idsrv3test.pfx"), "idsrv3test");

            var builder = services
                .AddIdentityServer()
                .SetSigningCredential(certificate)
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryScopes(Scopes.Get())
                .AddInMemoryUsers(Users.Get());


            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
        }
    }
}
