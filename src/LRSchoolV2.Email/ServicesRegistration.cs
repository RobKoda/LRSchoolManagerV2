// ReSharper disable UnusedType.Global - Intended use is external
// ReSharper disable UnusedMember.Global - Intended use is external

using LRSchoolV2.Email.Abstractions;
using LRSchoolV2.Email.Models;
using LRSchoolV2.Email.Services;
using MailKit.Net.Smtp;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace LRSchoolV2.Email;

public static class ServicesRegistration
{
    public static void AddEmailServices(this IServiceCollection inServices, string inConfigurationSectionName = "EmailConfiguration")
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ServicesRegistration).Assembly);
        inServices
            .AddOptions<EmailConfiguration>()
            .BindConfiguration(inConfigurationSectionName);
        inServices.AddTransient<IEmailSender, EmailSender>();
        inServices.AddTransient<ISmtpClient, SmtpClient>();
    }
}