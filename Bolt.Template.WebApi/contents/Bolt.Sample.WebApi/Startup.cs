using Bolt.Sample.WebApi.Infrastructure.SwaggerEx;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Bolt.Common.Extensions;
using Bolt.Sample.WebApi.Infrastructure;

namespace Bolt.Sample.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceConfiguration.Configure(services);

            services.AddHttpContextAccessor();

            services.AddControllers()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.ApplyBasicOptions());

            services.AddHealthChecks();
            services.AddDefaultSwaggerGen(AppDomain.CurrentDomain.FriendlyName, "v1");
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(UnhandledErrorHandler.Handle<Startup>);
            }
            
            app.UsePathBase("/__app_name__");
            
            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapPing();
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseDefaultSwaggerUI(AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
