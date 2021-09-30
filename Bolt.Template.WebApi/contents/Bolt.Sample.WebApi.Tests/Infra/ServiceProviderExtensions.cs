using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Give you service registered in service provider
        /// and clear received calls based on passed boolean flag
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clearReceivedCalls">default is true</param>
        /// <returns></returns>
        public static T GetFakeService<T>(this IHaveServiceProvider source, bool clearReceivedCalls = true) where T : class
        {
            var rsp = source.ServiceProvider.GetRequiredService<T>();
            if (clearReceivedCalls)
            {
                rsp.ClearReceivedCalls();
                rsp.ClearSubstitute();
            }
            return rsp;
        }


        /// <summary>
        /// Return service registered in service provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>(this IHaveServiceProvider source) where T : class
        {
            return source.ServiceProvider.GetRequiredService<T>();
        }
    }
}
