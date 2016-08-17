using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ecom.API.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Ecom.API.Infrastructures.Mappings;
using Ecom.API.Infrastructures.Extensions;
using Ecom.API.Repository;
using Ecom.API.Repository.Abstract;

namespace Ecom.API
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


        public IConfigurationRoot Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BaseContext>(options =>
                    options.UseSqlServer(Configuration["ConnectionString:DefaultConnection"],
                    b => b.MigrationsAssembly("Ecom.API")));

            //Add Services in IoC Container
            services.AddScoped(typeof(BaseContext));
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // Automapper Configuration
            AutoMapperConfiguration.Configure();

            //Enable CORS
            services.AddCors();

            ////Define policy for CORS
            //services.AddCors(options => {
            //    options.AddPolicy("AllowSpecificOrigins",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://example.com", "http://www.contoso.com");
            //        });
            //});

            // Inject an implementation of ISwaggerProvider
            services.AddSwaggerServices(Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Add JwtBearer authentication package
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Authority = "http://localhost:16619/",
                Audience = "http://localhost:16619/resources",
                RequireHttpsMetadata = false,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true

                //TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters().... use if more validation needed
            });

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enabling CORS for all origin
            app.UseCors(builder =>
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            // //Enabling CORS for specific origins
            // app.UseCors(builder =>
            //{
            //    builder.WithOrigins("https://contoso.com", "https://janaks.com.np")
            //    .AllowAnyHeader()
            //    .AllowAnyMethod();
            //});

            //Enabling CORS based on CORS policy
            //app.UseCors("AllowSpecificOrigins");

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();

        }
    }
}
