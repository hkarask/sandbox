using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SwashbuckleReDocDemo.Api.Extensions
{
    public static class StartupExtenstions
    {
        public static void AddErrorHandling(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(new HttpResponseExceptionFilter());
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    //https://docs.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0
                    options.SuppressModelStateInvalidFilter = true;
                });
        }
    }
}