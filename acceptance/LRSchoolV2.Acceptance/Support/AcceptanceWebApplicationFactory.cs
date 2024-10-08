﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace LRSchoolV2.Acceptance.Support;

public class AcceptanceWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
    where TStartup : class
{
    private const string SettingsFile = "appsettings.Acceptance.json";

    protected override void ConfigureWebHost(IWebHostBuilder inBuilder)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), SettingsFile))
            .AddUserSecrets(typeof(AcceptanceWebApplicationFactory<TStartup>).Assembly)
            .AddCommandLine(["--BypassAuth", string.Empty])
            .Build();
        inBuilder.UseConfiguration(configurationBuilder);
    }
}