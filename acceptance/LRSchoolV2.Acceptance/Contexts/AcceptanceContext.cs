using LRSchoolV2.Acceptance.Support;
using LRSchoolV2.Blazor;
using Microsoft.Extensions.DependencyInjection;

namespace LRSchoolV2.Acceptance.Contexts;

// ReSharper disable once ClassNeverInstantiated.Global - Auto instantiated by SpecFlow
public sealed class AcceptanceContext
{
    public IServiceProvider ServiceProvider { get; }
    public HttpClient HttpClient { get; }
    
    public AcceptanceContext()
    {
        var acceptanceWebApplicationFactory = new AcceptanceWebApplicationFactory<Program>();
        
        ServiceProvider = acceptanceWebApplicationFactory.Services.CreateScope().ServiceProvider;
        HttpClient = acceptanceWebApplicationFactory.CreateClient();
    }
}