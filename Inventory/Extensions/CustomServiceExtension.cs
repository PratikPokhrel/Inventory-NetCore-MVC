using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Web.Extensions
{
    public static class CustomServiceExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            return services;
        }
    }
}
