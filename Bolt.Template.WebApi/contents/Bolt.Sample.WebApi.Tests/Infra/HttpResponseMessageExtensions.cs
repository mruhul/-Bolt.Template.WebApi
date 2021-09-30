using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bolt.Common.Extensions;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage msg)
            => (await msg.Content.ReadAsStringAsync()).DeserializeFromJson<T>();
    }
}
