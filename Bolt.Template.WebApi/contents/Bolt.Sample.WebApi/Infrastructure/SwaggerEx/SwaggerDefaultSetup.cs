using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bolt.Sample.WebApi.Infrastructure.SwaggerEx
{
    public static class SwaggerDefaultSetup
    {
        public static IServiceCollection AddDefaultSwaggerGen(this IServiceCollection services, string appName, string version)
        {
            return services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();

                c.OperationFilter<CustomHeaderOperationFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = version });

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                var xmlFile = "Bolt.Sample.WebApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder UseDefaultSwaggerUI(this IApplicationBuilder app, string appName, string version = "v1")
        {
            return app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName} {version}");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
