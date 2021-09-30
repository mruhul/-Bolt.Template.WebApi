using System;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public interface IHaveServiceProvider
    {
        IServiceProvider ServiceProvider { get; }
    }
}
