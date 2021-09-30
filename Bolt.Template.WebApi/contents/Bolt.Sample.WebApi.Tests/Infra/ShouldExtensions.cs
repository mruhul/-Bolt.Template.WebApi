using Bolt.Common.Extensions;
using Shouldly;

namespace Bolt.Sample.WebApi.Tests.Infra
{
    public static class ShouldExtensions
    {
        public static void ShouldMatchApprovedEx<T>(this T got, string msg = null, string discriminator = null)
        {
            got.SerializeToPrettyJson().ShouldMatchApproved(c =>
            {
                c.UseCallerLocation();
                c.WithStringCompareOptions(StringCompareShould.IgnoreLineEndings);
                if (!string.IsNullOrWhiteSpace(discriminator))
                {
                    c.WithDiscriminator(discriminator);
                }
            }, msg);
        }
    }
}
