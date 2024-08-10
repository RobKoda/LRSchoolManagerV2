using System.Diagnostics.CodeAnalysis;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServices;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations;
using LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices;
using LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments;
using LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits;
using LRSchoolV2.Infrastructure.Common.Addresses;
using LRSchoolV2.Infrastructure.Common.Documents;
using LRSchoolV2.Infrastructure.Common.SchoolYears;
using LRSchoolV2.Infrastructure.Consultants.Consultants;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems;
using LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices;
using LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems;
using LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes;
using LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations;
using LRSchoolV2.Infrastructure.Persons.PersonRegistrations;
using LRSchoolV2.Infrastructure.Persons.Persons;
using Microsoft.EntityFrameworkCore;

// ReSharper disable ReturnTypeCanBeEnumerable.Global - EF Core requirement
namespace LRSchoolV2.Infrastructure;

[ExcludeFromCodeCoverage]
public class ApplicationContext(DbContextOptions inOptions) : DbContext(inOptions)
{
    public DbSet<AddressDataModel> Addresses => Set<AddressDataModel>();
    public DbSet<AnnualServiceDataModel> AnnualServices => Set<AnnualServiceDataModel>();
    public DbSet<AnnualServiceConsultantWorkDataModel> AnnualServiceConsultantWorks => Set<AnnualServiceConsultantWorkDataModel>();
    public DbSet<AnnualServiceVariationDataModel> AnnualServiceVariations => Set<AnnualServiceVariationDataModel>();
    public DbSet<AnnualServiceVariationConsultantWorkDataModel> AnnualServiceVariationConsultantWorks => Set<AnnualServiceVariationConsultantWorkDataModel>();
    public DbSet<AnnualServiceVariationYearlyPriceDataModel> AnnualServiceVariationYearlyPrices => Set<AnnualServiceVariationYearlyPriceDataModel>();
    public DbSet<CheckDepositDataModel> CheckDeposits => Set<CheckDepositDataModel>();
    public DbSet<CheckDepositPaymentDataModel> CheckDepositPayments => Set<CheckDepositPaymentDataModel>();
    public DbSet<ConsultantDataModel> Consultants => Set<ConsultantDataModel>();
    //public DbSet<ConsultantQuoteDataModel> ConsultantQuotes => Set<ConsultantQuoteDataModel>();
    //public DbSet<ConsultantQuoteItemDataModel> ConsultantQuoteItems => Set<ConsultantQuoteItemDataModel>();
    public DbSet<CustomerInvoiceDataModel> CustomerInvoices => Set<CustomerInvoiceDataModel>();
    public DbSet<CustomerInvoiceItemDataModel> CustomerInvoiceItems => Set<CustomerInvoiceItemDataModel>();
    public DbSet<CustomerQuoteDataModel> CustomerQuotes => Set<CustomerQuoteDataModel>();
    public DbSet<CustomerQuoteItemDataModel> CustomerQuoteItems => Set<CustomerQuoteItemDataModel>();
    public DbSet<CustomerPaymentDataModel> CustomerPayments => Set<CustomerPaymentDataModel>();
    public DbSet<DocumentDataModel> Documents => Set<DocumentDataModel>();
    public DbSet<PersonDataModel> Persons => Set<PersonDataModel>();
    public DbSet<PersonRegistrationDataModel> PersonRegistrations => Set<PersonRegistrationDataModel>();
    public DbSet<PersonAnnualServiceVariationDataModel> PersonAnnualServiceVariations => Set<PersonAnnualServiceVariationDataModel>();
    public DbSet<SchoolYearDataModel> SchoolYears => Set<SchoolYearDataModel>();
    
    protected override void OnModelCreating(ModelBuilder inModelBuilder)
    {
        base.OnModelCreating(inModelBuilder);

        foreach (var relationship in inModelBuilder.Model.GetEntityTypes()
                     .SelectMany(inMutableEntityType => inMutableEntityType.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder inConfigurationBuilder)
    {
        base.ConfigureConventions(inConfigurationBuilder);
        inConfigurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        inConfigurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
    }
}