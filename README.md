# APM
Cortlex Application Performance Management

[![Build Status](https://dev.azure.com/cortlex/APM/_apis/build/status/APM%20Build?branchName=master)](https://dev.azure.com/cortlex/APM/_build/latest?definitionId=12&branchName=master)

## Adding Healthcheck packages.

``` PowerShell
Install-Package AspNetCore.HealthChecks.System
Install-Package AspNetCore.HealthChecks.Network
Install-Package AspNetCore.HealthChecks.SqlServer
Install-Package AspNetCore.HealthChecks.MongoDb
Install-Package AspNetCore.HealthChecks.Npgsql
Install-Package AspNetCore.HealthChecks.Elasticsearch
Install-Package AspNetCore.HealthChecks.Redis
Install-Package AspNetCore.HealthChecks.EventStore
Install-Package AspNetCore.HealthChecks.AzureStorage
Install-Package AspNetCore.HealthChecks.AzureServiceBus
Install-Package AspNetCore.HealthChecks.AzureKeyVault
Install-Package AspNetCore.HealthChecks.MySql
Install-Package AspNetCore.HealthChecks.DocumentDb
Install-Package AspNetCore.HealthChecks.SqLite
Install-Package AspNetCore.HealthChecks.RavenDB
Install-Package AspNetCore.HealthChecks.Kafka
Install-Package AspNetCore.HealthChecks.RabbitMQ
Install-Package AspNetCore.HealthChecks.OpenIdConnectServer
Install-Package AspNetCore.HealthChecks.DynamoDB
Install-Package AspNetCore.HealthChecks.Oracle
Install-Package AspNetCore.HealthChecks.Uris
Install-Package AspNetCore.HealthChecks.Aws.S3
Install-Package AspNetCore.HealthChecks.Consul
Install-Package AspNetCore.HealthChecks.Hangfire
Install-Package AspNetCore.HealthChecks.SignalR
Install-Package AspNetCore.HealthChecks.Kubernetes
Install-Package AspNetCore.HealthChecks.Gcp.CloudFirestore
```



Once the package is installed you can add the HealthCheck using the **AddXXX** IServiceCollection extension methods.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddHealthChecks()
        .AddSqlServer(Configuration["Data:ConnectionStrings:Sql"])
        .AddRedis(Configuration["Data:ConnectionStrings:Redis"]);
}
```

## Add Environment field to start up class
```csharp
public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }
```

## Adding InfluxDb Publisher.
```PowerShell
Install-Package Cortlex.APM.Health.Publishers.InfluxDb -Version 1.0.4-alpha
```


```csharp
    services.AddHealthChecks()
        .AddSqlServer(Configuration["Data:ConnectionStrings:Sql"])
        .AddRedis(Configuration["Data:ConnectionStrings:Redis"]);
		.AddInfluxPublisher(Environment.ApplicationName, Environment.EnvironmentName, "localhost:8888", "login", "password");
```


## Workaround for Asp.NET Core (before 2.2) 
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-2.2

You must add following code at Startup.cs

```csharp
private const string HealthCheckServiceAssembly = "Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckPublisherHostedService";
```

Add following code inside of ConfigureServices method in Startup.cs
```csharp
// The following workaround permits adding an IHealthCheckPublisher 
                // instance to the service container when one or more other hosted 
                // services have already been added to the app. This workaround
                services.TryAddEnumerable(
                    ServiceDescriptor.Singleton(typeof(IHostedService),
                    typeof(HealthCheckPublisherOptions).Assembly
                        .GetType(HealthCheckServiceAssembly)));
```

