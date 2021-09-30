using System.Diagnostics;
using Bolt.Common.Extensions;
using Bolt.IocScanner.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Serilog.Core;
using Serilog.Events;

namespace Bolt.Sample.WebApi.Infrastructure.SerilogEx
{
    [AutoBind]
    public class DefaultEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private const string RouteNameTenant = "tenant";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("cid", Activity.Current?.RootId ?? _httpContextAccessor.HttpContext?.TraceIdentifier));

            var tenant = _httpContextAccessor.HttpContext?.GetRouteValue(RouteNameTenant)?.ToString();

            if (string.IsNullOrWhiteSpace(tenant) is false)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(RouteNameTenant, tenant));
            }

            var userId = _httpContextAccessor.HttpContext?.User?.Identity?.Name
                ?? _httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValueOrDefault(Constants.HeaderNames.AppId);
            if (string.IsNullOrWhiteSpace(userId) is false)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("userid", userId));
            }

            var consumerId = _httpContextAccessor.HttpContext?.Request?.Headers?.TryGetValueOrDefault(Constants.HeaderNames.AppId);
            if (string.IsNullOrWhiteSpace(consumerId) is false)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("consumer", consumerId.ToString()));
            }
        }
    }
}
