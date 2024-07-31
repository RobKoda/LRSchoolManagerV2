using System.Net;
using LRSchoolV2.Acceptance.Contexts;

namespace LRSchoolV2.Acceptance.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global - Auto instantiated by SpecFlow
public class HealthcheckDriver(AcceptanceContext inContext)
{
    private HttpResponseMessage? _response;
    
    public async Task GetHealthcheckAsync() =>
        _response = await inContext.HttpClient.GetAsync("/api/Healthcheck");

    public HttpStatusCode GetHealthcheckStatusCode() => 
        _response!.StatusCode;
}