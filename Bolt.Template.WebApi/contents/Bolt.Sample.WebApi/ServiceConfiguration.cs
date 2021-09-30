using Microsoft.Extensions.DependencyInjection;
using Bolt.IocScanner;
using Bolt.RequestBus;

namespace Bolt.Sample.WebApi
{
    public static class ServiceConfiguration
    {
        public static void Configure(this IServiceCollection services)
        {
            services.Scan<Startup>(new IocScannerOptions { SkipWhenAutoBindMissing = true });
#if (!excludeSampleCode)
            services.AddRequestBus();
#endif
        }
    }
}
