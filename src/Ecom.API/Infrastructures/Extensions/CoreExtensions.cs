using Ecom.API.Infrastructures.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;
using System.Collections.Generic;

namespace Ecom.API.Infrastructures.Extensions
{
    public static class CoreExtensions
    {
        /// <summary>
        /// Get validation errors from model state
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetErrors(this ModelStateDictionary modelState)
        {
            var stateValues = modelState.Values;
            List<string> errors = new List<string>();


            foreach (var errorItem in stateValues)
            {
                foreach (var error in errorItem.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return errors;
        }

        /// <summary>
        /// Configure Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env">Root path of XML documentation</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IHostingEnvironment env)
        {
            string swaggerCommentXmlPath = string.Empty;
            if (env.IsDevelopment()) //development
                swaggerCommentXmlPath = $@"{env.ContentRootPath}\bin\Debug\netcoreapp1.0\Ecom.API.xml";
            else //production
                swaggerCommentXmlPath = $@"{env.ContentRootPath}\Ecom.API.xml";


            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Ecom API",
                    Description = "Ecom API demo at Dev/Tech Day.",
                    TermsOfService = "NA",
                    Contact = new Contact() { Name = "@janaks09", Url = "http://janaks.com.np" },
                });

                options.IncludeXmlComments(swaggerCommentXmlPath);
                options.OperationFilter<AuthOperationFilter>();
                options.DescribeAllEnumsAsStrings();
            });

            return services;
        }
    }
}
