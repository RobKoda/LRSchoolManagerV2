﻿// <auto-generated />
using System;
using LRSchoolV2.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LRSchoolV2.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks.AnnualServiceConsultantWorkDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnnualServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("CommonWorkHours")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CommonWorkHoursComment")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid>("ConsultantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnnualServiceId");

                    b.HasIndex("ConsultantId");

                    b.HasIndex("SchoolYearId");

                    b.ToTable("AnnualServiceConsultantWork");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks.AnnualServiceVariationConsultantWorkDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnnualServiceVariationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConsultantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("IndividualWorkHours")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("IndividualWorkHoursComment")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnnualServiceVariationId");

                    b.HasIndex("ConsultantId");

                    b.HasIndex("SchoolYearId");

                    b.ToTable("AnnualServiceVariationConsultantWork");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices.AnnualServiceVariationYearlyPriceDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnnualServiceVariationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Margin")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnnualServiceVariationId");

                    b.HasIndex("SchoolYearId");

                    b.ToTable("AnnualServiceVariationYearlyPrice");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnnualServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("InvoiceName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AnnualServiceId");

                    b.ToTable("AnnualServiceVariation");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServices.AnnualServiceDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("AnnualService");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments.CheckDepositPaymentDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CheckDepositId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerPaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CheckDepositId");

                    b.HasIndex("CustomerPaymentId");

                    b.ToTable("CheckDepositPayment");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits.CheckDepositDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("CheckDeposit");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Common.Addresses.AddressDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("StreetComplement")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Common.Documents.DocumentDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<byte[]>("FileContent")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("MembershipFee")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Consultants.Consultants.ConsultantDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BicCode")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Consultant");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems.CustomerInvoiceItemDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerInvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Denomination")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerInvoiceId");

                    b.ToTable("CustomerInvoiceItem");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices.CustomerInvoiceDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailSent")
                        .HasColumnType("bit");

                    b.Property<string>("InvoiceCustomerAddress")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("InvoiceCustomerName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerInvoice");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments.CustomerPaymentDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CustomerPaymentTypeValue")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("CustomerPayment");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems.CustomerQuoteItemDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerQuoteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Denomination")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("ReferenceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerQuoteId");

                    b.ToTable("CustomerQuoteItem");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes.CustomerQuoteDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailSent")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("QuoteCustomerAddress")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("QuoteCustomerName")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerQuote");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations.PersonAnnualServiceVariationDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AnnualServiceVariationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BilledPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsFullyBilled")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentsCount")
                        .HasColumnType("int");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AnnualServiceVariationId");

                    b.HasIndex("BilledPersonId");

                    b.HasIndex("PersonId");

                    b.HasIndex("SchoolYearId");

                    b.ToTable("PersonAnnualServiceVariation");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.PersonRegistrations.PersonRegistrationDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("BilledPersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("ImageRightsGranted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFullyBilled")
                        .HasColumnType("bit");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SchoolYearId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BilledPersonId");

                    b.HasIndex("PersonId");

                    b.HasIndex("SchoolYearId");

                    b.ToTable("PersonRegistration");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("BillingToContactPerson1")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ContactPerson1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContactPerson2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ContactPerson1Id");

                    b.HasIndex("ContactPerson2Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceConsultantWorks.AnnualServiceConsultantWorkDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.AnnualServices.AnnualServices.AnnualServiceDataModel", "AnnualService")
                        .WithMany("ConsultantWorks")
                        .HasForeignKey("AnnualServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Consultants.Consultants.ConsultantDataModel", "Consultant")
                        .WithMany()
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", "SchoolYear")
                        .WithMany()
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnnualService");

                    b.Navigation("Consultant");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationConsultantWorks.AnnualServiceVariationConsultantWorkDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", "AnnualServiceVariation")
                        .WithMany("ConsultantWorks")
                        .HasForeignKey("AnnualServiceVariationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Consultants.Consultants.ConsultantDataModel", "Consultant")
                        .WithMany()
                        .HasForeignKey("ConsultantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", "SchoolYear")
                        .WithMany()
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnnualServiceVariation");

                    b.Navigation("Consultant");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariationYearlyPrices.AnnualServiceVariationYearlyPriceDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", "AnnualServiceVariation")
                        .WithMany("YearlyPrices")
                        .HasForeignKey("AnnualServiceVariationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", "SchoolYear")
                        .WithMany()
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnnualServiceVariation");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.AnnualServices.AnnualServices.AnnualServiceDataModel", "AnnualService")
                        .WithMany("Variations")
                        .HasForeignKey("AnnualServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnnualService");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CheckDeposits.CheckDepositPayments.CheckDepositPaymentDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits.CheckDepositDataModel", "CheckDeposit")
                        .WithMany("CheckDepositPayments")
                        .HasForeignKey("CheckDepositId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments.CustomerPaymentDataModel", "CustomerPayment")
                        .WithMany()
                        .HasForeignKey("CustomerPaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CheckDeposit");

                    b.Navigation("CustomerPayment");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Consultants.Consultants.ConsultantDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Common.Addresses.AddressDataModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoiceItems.CustomerInvoiceItemDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices.CustomerInvoiceDataModel", "CustomerInvoice")
                        .WithMany("Items")
                        .HasForeignKey("CustomerInvoiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CustomerInvoice");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices.CustomerInvoiceDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "Customer")
                        .WithMany("CustomerInvoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerPayments.CustomerPayments.CustomerPaymentDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "Person")
                        .WithMany("CustomerPayments")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuoteItems.CustomerQuoteItemDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes.CustomerQuoteDataModel", "CustomerQuote")
                        .WithMany("Items")
                        .HasForeignKey("CustomerQuoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CustomerQuote");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes.CustomerQuoteDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.PersonAnnualServiceVariations.PersonAnnualServiceVariationDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", "AnnualServiceVariation")
                        .WithMany("PersonAnnualServiceVariations")
                        .HasForeignKey("AnnualServiceVariationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "BilledPerson")
                        .WithMany()
                        .HasForeignKey("BilledPersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", "SchoolYear")
                        .WithMany()
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AnnualServiceVariation");

                    b.Navigation("BilledPerson");

                    b.Navigation("Person");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.PersonRegistrations.PersonRegistrationDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "BilledPerson")
                        .WithMany()
                        .HasForeignKey("BilledPersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "Person")
                        .WithMany("Registrations")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Common.SchoolYears.SchoolYearDataModel", "SchoolYear")
                        .WithMany()
                        .HasForeignKey("SchoolYearId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BilledPerson");

                    b.Navigation("Person");

                    b.Navigation("SchoolYear");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", b =>
                {
                    b.HasOne("LRSchoolV2.Infrastructure.Common.Addresses.AddressDataModel", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "ContactPerson1")
                        .WithMany()
                        .HasForeignKey("ContactPerson1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", "ContactPerson2")
                        .WithMany()
                        .HasForeignKey("ContactPerson2Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Address");

                    b.Navigation("ContactPerson1");

                    b.Navigation("ContactPerson2");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServiceVariations.AnnualServiceVariationDataModel", b =>
                {
                    b.Navigation("ConsultantWorks");

                    b.Navigation("PersonAnnualServiceVariations");

                    b.Navigation("YearlyPrices");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.AnnualServices.AnnualServices.AnnualServiceDataModel", b =>
                {
                    b.Navigation("ConsultantWorks");

                    b.Navigation("Variations");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CheckDeposits.CheckDeposits.CheckDepositDataModel", b =>
                {
                    b.Navigation("CheckDepositPayments");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerInvoices.CustomerInvoices.CustomerInvoiceDataModel", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.CustomerQuotes.CustomerQuotes.CustomerQuoteDataModel", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LRSchoolV2.Infrastructure.Persons.Persons.PersonDataModel", b =>
                {
                    b.Navigation("CustomerInvoices");

                    b.Navigation("CustomerPayments");

                    b.Navigation("Registrations");
                });
#pragma warning restore 612, 618
        }
    }
}
