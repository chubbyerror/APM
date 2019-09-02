using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InfluxData.Net.Common.Enums;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cortlex.APM.Health.Publishers.InfluxDb
{
    public class InfluxDbPublisher : IHealthCheckPublisher
    {
        public InfluxDbPublisher(string endpoint, string login, string password, InfluxDbVersion dbVersion, string dbName)
        {
            Endpoint = endpoint;
            Login = login;
            Password = password;
            DbVersion = dbVersion;
            DbName = dbName;
        }

        private string Endpoint { get; }

        private string Login { get; }

        private string Password { get; }

        private string DbName { get; }

        private InfluxDbVersion DbVersion { get; }

        public async Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            try
            {
                var influxDbClient = new InfluxDbClient(Endpoint, Login, Password, DbVersion);

                var points = GeneratePoints(report);

                if (!cancellationToken.IsCancellationRequested)
                {
                    await influxDbClient.Client.WriteAsync(points, DbName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Point> GeneratePoints(HealthReport report)
        {
            var points = new List<Point>();

            foreach (var entry in report.Entries)
            {
                var pointToWrite = new Point()
                {
                    Name = "application.health",
                    Tags = new Dictionary<string, object>()
                    {
                        { "app", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name },
                        { "server", System.Environment.MachineName },
                        { "env", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT ") },
                        { "CheckType", entry.Key },
                        { "HealthStatusString", entry.Value.Status.ToString() }
                    },
                    Fields = new Dictionary<string, object>()
                    {
                        { "HealthStatusInt", (int)entry.Value.Status },
                        { "HealthStatusString", entry.Value.Status.ToString() },
                    },
                    Timestamp = DateTime.UtcNow
                };

                points.Add(pointToWrite);
            }

            return points;
        }
    }
}
