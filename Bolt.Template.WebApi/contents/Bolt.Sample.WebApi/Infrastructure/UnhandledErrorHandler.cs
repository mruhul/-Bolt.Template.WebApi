using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace Bolt.Sample.WebApi.Infrastructure
{
    public static class UnhandledErrorHandler
    {
        public static void Handle<T>(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var logger = context.RequestServices.GetService<ILogger<T>>();

                logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("An unhandled error occurred.");
            });
        }
    }
}
