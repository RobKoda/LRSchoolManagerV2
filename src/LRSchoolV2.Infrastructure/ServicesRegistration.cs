using LRSchoolV2.Application.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LRSchoolV2.Infrastructure;

public static class ServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection inServices, IConfiguration inConfiguration)
    {
        AddRepositories(inServices);

        inServices.AddDbContextFactory<ApplicationContext>(inOptions =>
        {
            inOptions.UseSqlServer(inConfiguration.GetConnectionString("DefaultConnection"));
#if DEBUG
            inOptions.EnableSensitiveDataLogging();
#endif
        });
    }

    private static void AddRepositories(IServiceCollection inServices)
    {
        var repositories = typeof(ServicesRegistration).Assembly.DefinedTypes
            .Where(inTypeInfo => inTypeInfo.GetInterfaces().Any(inInterface => inInterface == typeof(IRepository)));
        
        foreach (var injectableType in repositories)
        {
            var repositoryInterface = injectableType.ImplementedInterfaces.Single(inInterface => inInterface != typeof(IRepository));
            inServices.AddScoped(repositoryInterface, injectableType);
        }
    }
}