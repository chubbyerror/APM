using InfluxData.Net.Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cortlex.APM.Health.Publishers.InfluxDb
{
    public static class InfluxHealthCheckBuilderExtensions
    {
        public static IHealthChecksBuilder AddInfluxPublisher(this IHealthChecksBuilder builder, string endpoint,
            string login, string password, InfluxDbVersion dbVersion = InfluxDbVersion.Latest,
            string dbName = "_internal")
        {
            builder.Services.AddSingleton(sp =>
                (IHealthCheckPublisher) new InfluxDbPublisher(endpoint, login, password, dbVersion, dbName));
            return builder;
        }
    }
}
