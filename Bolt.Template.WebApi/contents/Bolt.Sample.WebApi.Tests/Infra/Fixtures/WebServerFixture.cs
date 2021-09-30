using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Bolt.Sample.WebApi.Tests.Infra.Fixtures
{
    public class WebServerFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            builder.UseEnvironment("test-local");
            builder.ConfigureTestServices(FakeServiceSetup.ForWebServer.Run);

        }
    }

    [CollectionDefinition(nameof(WebServerFixtureCollection), DisableParallelization = true)]
    public class WebServerFixtureCollection : ICollectionFixture<WebServerFixture>
    {

    }
}
