using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(config => 
            {
                config.RegisterServicesFromAssembly(assembly);
            });

            services.AddAutoMapper(config =>
            {
                config.AddMaps(assembly);
            });
            return services;
        }
    }
}
