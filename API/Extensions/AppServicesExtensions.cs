using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Infrastructure.Repository;
using API.Errors;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options =>
           {
               options.InvalidModelStateResponseFactory = actionContext =>
               {
                   var errors = actionContext.ModelState
                   .Where(err => err.Value.Errors.Count > 0)
                   .SelectMany(err => err.Value.Errors)
                   .Select(err => err.ErrorMessage).ToArray();

                   var errResponse = new RequestValidator
                   {
                       Errors = errors
                   };

                   return new BadRequestObjectResult(errResponse);
               };
           });

            return services;
        }
    }
}