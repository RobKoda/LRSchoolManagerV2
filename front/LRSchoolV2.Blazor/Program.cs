using System.Globalization;
using BlazorDownloadFile;
using FluentValidation;
using LRSchoolV2.Application;
using LRSchoolV2.Authentication;
using LRSchoolV2.Blazor.Shared;
using LRSchoolV2.Email;
using LRSchoolV2.Infrastructure;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
#if RELEASE
using Microsoft.EntityFrameworkCore;
#endif
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

AddMappingConfigs();
AddValidators();
AddMappingConfigs();
AddPageServices();
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddEmailServices();

builder.Services.AddMudServices();
builder.Services.AddBlazorDownloadFile();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

// This allows acceptance test to run
if (args.All(inArgument => string.Compare(inArgument, "--BypassAuth=", StringComparison.InvariantCulture) != 0))
{
    AddAuthenticationServices();   
}

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { new CultureInfo("fr-FR") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("fr-FR"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await EnsureDatabaseCreatedAsync();

app.Run();
return;

void AddMappingConfigs()
{
    var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
    typeAdapterConfig.Scan(typeof(LRSchoolV2.Application.ServicesRegistration).Assembly);
    typeAdapterConfig.Scan(typeof(LRSchoolV2.Infrastructure.ServicesRegistration).Assembly);
    typeAdapterConfig.Scan(typeof(LRSchoolV2.Blazor.App).Assembly);
}

void AddValidators()
{
    builder.Services.AddValidatorsFromAssembly(typeof(LRSchoolV2.Application.ServicesRegistration).Assembly, ServiceLifetime.Transient);
    ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
    ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
}

async Task EnsureDatabaseCreatedAsync()
{
    var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();
#if DEBUG
    await context.Database.EnsureCreatedAsync();
#else
    await context.Database.MigrateAsync();
#endif
}

void AddAuthenticationServices()
{
    builder.Services
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie();
    builder.Services.AddAuthentication().AddGoogle(inOptions =>
        {
            inOptions.ClientId = builder.Configuration["Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not found");
            inOptions.ClientSecret = builder.Configuration["Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not found");
            inOptions.ClaimActions.MapJsonKey("urn:google:profile", "link");
            inOptions.ClaimActions.MapJsonKey("urn:google:image", "picture");
        }
    );
    builder.Services.AddAuthorizationBuilder()
        .AddPolicy("EmailPolicy", inPolicy =>
            inPolicy.Requirements.Add(new EmailRequirement()))
        .SetDefaultPolicy(new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .AddRequirements(new EmailRequirement())
            .Build());
    builder.Services.AddSingleton<IAuthorizationHandler, EmailAuthorizationHandler>();
}

void AddPageServices()
{
    var frontDataServices = typeof(LRSchoolV2.Blazor.App).Assembly.DefinedTypes
        .Where(inTypeInfo => inTypeInfo.GetInterfaces().Any(inInterface => inInterface == typeof(IFrontDataService)));
    
    foreach (var injectableType in frontDataServices)
    {
        builder.Services.AddTransient(injectableType);
    }
}

// ReSharper disable All - used in AcceptanceContext
#pragma warning disable CA1050
namespace LRSchoolV2.Blazor
{
    public partial class Program
    {
    }
}
#pragma warning restore CA1050