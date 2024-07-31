using LRSchoolV2.Acceptance.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using TechTalk.SpecFlow;

namespace LRSchoolV2.Acceptance.Hooks;

[Binding]
public class RespawnHook(AcceptanceContext inContext)
{
    private static Respawner? _respawner;
    private static string _connectionString = string.Empty;
    
    [BeforeScenario]
    public async Task RespawnDatabaseBeforeScenario()
    {
        var configuration = inContext.ServiceProvider.GetRequiredService<IConfiguration>();
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        await ResetCheckpoint();
    }

    private static async Task ResetCheckpoint()
    {
        _respawner ??= await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            CheckTemporalTables = true
        });
        await _respawner.ResetAsync(_connectionString);
    }

    [AfterTestRun]
    public static async Task RespawnDatabaseAfterTestRun() =>
        await ResetCheckpoint();
}