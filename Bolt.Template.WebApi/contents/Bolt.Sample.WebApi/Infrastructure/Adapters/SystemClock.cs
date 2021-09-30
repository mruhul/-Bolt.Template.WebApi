using System;
using Bolt.IocScanner.Attributes;
using Bolt.Sample.WebApi.Features.Shared.Ports;

namespace Bolt.Sample.WebApi.Infrastructure.Adapters
{
    [AutoBind]
    internal sealed class SystemClock : ISystemClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
