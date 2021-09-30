using System;
using Bolt.Sample.WebApi.Features.Shared.Ports;
using NSubstitute;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class SystemClockFake
    {
        public static DateTime GivenCurrentTime(this IHaveServiceProvider source, DateTime? utcNow = null)
        {
            var fake = source.GetFakeService<ISystemClock>();
            
            utcNow = utcNow ?? new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            fake.UtcNow.Returns(utcNow.Value);

            return utcNow.Value;
        }
    }
}
