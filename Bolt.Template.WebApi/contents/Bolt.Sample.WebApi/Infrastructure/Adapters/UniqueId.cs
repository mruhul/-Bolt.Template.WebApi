using System;
using Bolt.IocScanner.Attributes;
using Bolt.Sample.WebApi.Features.Shared.Ports;

namespace Bolt.Sample.WebApi.Infrastructure.Adapters
{
    [AutoBind]
    internal sealed class UniqueId : IUniqueId
    {
        public Guid New => Guid.NewGuid();
    }
}
