using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bolt.Sample.WebApi.Infrastructure;
using Bolt.Sample.WebApi.Tests.Infra.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Xunit;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    [Trait("Category", "Slow")]
    [Collection(nameof(WebServerFixtureCollection))]
    public abstract class TestWithWebServer : IHaveServiceProvider
    {
        private readonly WebServerFixture _fixture;

        protected TestWithWebServer(WebServerFixture fixture)
        {
            this._fixture = fixture;
        }

        IServiceProvider IHaveServiceProvider.ServiceProvider => _fixture.Services;

        protected HttpClient Server
        {
            get
            {
                var client = _fixture.CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    BaseAddress = new Uri("http://localhost/__app_name__"),
                    HandleCookies = false
                });

                client.DefaultRequestHeaders.Add(Constants.HeaderNames.AppId, "test-__app_name__");
                client.DefaultRequestHeaders.Add(Constants.HeaderNames.ApiKey, "7A50E91C-373C-412B-A893-AF4ED59A6FF1");

                return client;
            }
        }
    }
}
