using System;
using Bolt.Sample.WebApi.Features.Shared.Ports;
using NSubstitute;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class UniqueIdFake
    {
        public static Guid GivenUniqueId(this IHaveServiceProvider source, Guid? id = null)
        {
            var fake = source.GetFakeService<IUniqueId>();

            id = id ?? Guid.Parse("75D167D0-DC10-4B80-A649-9A7BC0F4CB16");

            fake.New.Returns(id.Value);

            return id.Value;
        }
    }
}
