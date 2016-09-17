using Ecom.API.Contexts;
using Ecom.API.Repository;
using Ecom.API.Repository.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace Ecom.API.Infrastructures.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Add Services in IoC Container
            services.AddScoped(typeof(BaseContext));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
