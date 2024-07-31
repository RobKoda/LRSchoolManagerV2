using Microsoft.Extensions.DependencyInjection;

namespace LRSchoolV2.Application;

public static class ServicesRegistration
{
    public static void RegisterApplication(this IServiceCollection inServices)
    {
        inServices.AddMediatR(inConfig => inConfig.RegisterServicesFromAssembly(typeof(ServicesRegistration).Assembly));
    }
}