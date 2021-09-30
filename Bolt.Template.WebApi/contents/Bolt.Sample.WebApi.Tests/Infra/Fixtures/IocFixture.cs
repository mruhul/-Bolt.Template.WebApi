using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bolt.Sample.WebApi.Tests.Infra.Fixtures
{
    public class IocFixture
    {
        private IServiceProvider _sp;

        public IocFixture()
        {
            var sc = new ServiceCollection();

            var config = new ConfigurationBuilder().Build();

            ServiceConfiguration.Configure(sc);

            SetupFakes(sc, config);

            _sp = sc.BuildServiceProvider();
        }

        private void SetupFakes(IServiceCollection services, IConfiguration configuration)
        {
            FakeServiceSetup.ForIocFixture.Run(services);
        }

        public IServiceScope CreateScope() => _sp.CreateScope();
    }



    [CollectionDefinition(nameof(IocFixtureCollection), DisableParallelization = false)]
    public class IocFixtureCollection : ICollectionFixture<IocFixture>
    {

    }
}
