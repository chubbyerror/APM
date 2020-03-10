using InfluxData.Net.Common.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cortlex.APM.Health.Publishers.InfluxDb
{
    public static class InfluxHealthCheckBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="applicationName">Value of IHostingEnvironment.ApplicationName</param>
        /// <param name="environmentName">Value of IHostingEnvironment.EnvironmentName</param>
        /// <param name="endpoint"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dbVersion"></param>
        /// <param name="dbName"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static IHealthChecksBuilder AddInfluxPublisher(this IHealthChecksBuilder builder, string applicationName, string environmentName, string endpoint,
            string login, string password, InfluxDbVersion dbVersion = InfluxDbVersion.Latest,
            string dbName = "_internal")
        {
            builder.Services.AddSingleton(sp =>
                (IHealthCheckPublisher) new InfluxDbPublisher(endpoint, login, password, dbVersion, dbName, applicationName, environmentName));
            return builder;
        }
    }
}
