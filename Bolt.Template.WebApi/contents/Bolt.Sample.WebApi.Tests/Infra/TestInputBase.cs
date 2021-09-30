namespace Bolt.Sample.WebApi.Tests.Infra
{
    /// <summary>
    /// Use this as base class for theory test input to display scenario in test report
    /// </summary>
    public class TestInputBase
    {
        private readonly string scenario;

        public TestInputBase(string scenario)
            => this.scenario = scenario;

        public override string ToString()
            => scenario;
    }
}
