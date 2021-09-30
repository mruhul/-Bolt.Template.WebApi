using Bolt.Sample.WebApi.Features.Shared.Ports;
using Bolt.Sample.WebApi.Features.Shared.Ports.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class FakeServiceSetup
    {
        public static class ForWebServer
        {
            public static void Run(IServiceCollection services)
            {
                services.Replace(ServiceDescriptor.Singleton(_ => Substitute.For<ISystemClock>()));
                services.Replace(ServiceDescriptor.Singleton(_ => Substitute.For<IUniqueId>()));

                // Add all fakes here for endpoint testing as singleton
                services.Replace(ServiceDescriptor.Singleton(_ => Substitute.For<IBooksRepository>()));
            }
        }

        public static class ForIocFixture
        {
            public static void Run(IServiceCollection services)
            {
                services.Replace(ServiceDescriptor.Scoped(_ => Substitute.For<ISystemClock>()));
                services.Replace(ServiceDescriptor.Scoped(_ => Substitute.For<IUniqueId>()));

                // Add all fakes here for non endpoint testing as singleton
                services.Replace(ServiceDescriptor.Scoped(_ => Substitute.For<IBooksRepository>()));
            }
        }
    }
}
