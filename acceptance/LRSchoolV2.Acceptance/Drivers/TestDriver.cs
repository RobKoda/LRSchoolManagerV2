using System.Net;
using LRSchoolV2.Acceptance.Support;

// ReSharper disable UnusedMember.Global - Might be used in the future

namespace LRSchoolV2.Acceptance.Drivers;

// ReSharper disable once UnusedType.Global - Might be used in the future
public abstract class TestDriver(FakeHttpClient inClient)
{
    // ReSharper disable once MemberCanBePrivate.Global - Might be used in the future
    protected readonly FakeHttpClient Client = inClient;
    
    public HttpStatusCode GetLastStatusCode() => 
        Client.HttpResponse!.StatusCode;
    
    public HttpContent GetLastResponseContent() => 
        Client.HttpResponse!.Content;
}