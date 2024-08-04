USE [master]
GO

BEGIN TRAN
GO

---------------------------------------------------------------------------------------------------------------------------------------------

DELETE FROM [LRSchoolV2_Dev].[dbo].[PersonAnnualServiceVariation]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[PersonRegistration]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[Person]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[AnnualServiceVariationYearlyPrice]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[AnnualServiceVariationConsultantWork]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[AnnualServiceVariation]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[AnnualServiceConsultantWork]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[AnnualService]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[Consultant]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[Address]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[Document]
GO
DELETE FROM [LRSchoolV2_Dev].[dbo].[SchoolYear]
GO

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'SCHOOL YEARS'

INSERT INTO [LRSchoolV2_Dev].[dbo].[SchoolYear] ([Id], [StartDate], [EndDate], [MembershipFee])
SELECT [Id], [StartDate], [EndDate], [MembershipFee]
FROM [LRSchool].[dbo].[SchoolYear]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'DOCUMENT'

INSERT INTO [LRSchoolV2_Dev].[dbo].[Document] ([Id], [ReferenceId], [FileName], [ContentType], [FileContent])
SELECT [Id], [ReferenceId], [FileName], [ContentType], [FileContent]
FROM [LRSchool].[dbo].[Document]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ADDRESSES'

INSERT INTO [LRSchoolV2_Dev].[dbo].[Address] ([Id], [Street], [StreetComplement], [ZipCode], [City])
SELECT [Id], [Street], [StreetComplement], [ZipCode], [City]
FROM [LRSchool].[dbo].[Address]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'CONSULTANTS'

INSERT INTO [LRSchoolV2_Dev].[dbo].[Consultant] ([Id], [LastName], [FirstName], [CompanyName], [PhoneNumber], [Email], [AddressId], [Iban], [BicCode])
SELECT [Id], [LastName], [FirstName], [CompanyName], [PhoneNumber], [Email], [AddressId], [Iban], [BicCode]
FROM [LRSchool].[dbo].[Consultant]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ANNUAL SERVICE'

INSERT INTO [LRSchoolV2_Dev].[dbo].[AnnualService] ([Id], [Name])
SELECT [Id], [Name]
FROM [LRSchool].[dbo].[AnnualService]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ANNUAL SERVICE CONSULTANT WORK'

INSERT INTO [LRSchoolV2_Dev].[dbo].[AnnualServiceConsultantWork] ([Id], [AnnualServiceId], [SchoolYearId], [ConsultantId], [CommonWorkHours], [CommonWorkHoursComment])
SELECT [Id], [AnnualServiceId], [SchoolYearId], [ConsultantId], [CommonWorkHours], [CommonWorkHoursComment]
FROM [LRSchool].[dbo].[AnnualServiceConsultantWork]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ANNUAL SERVICE VARIATION'

INSERT INTO [LRSchoolV2_Dev].[dbo].[AnnualServiceVariation] ([Id], [AnnualServiceId], [Name], [InvoiceName])
SELECT [Id], [AnnualServiceId], [Name], [InvoiceName]
FROM [LRSchool].[dbo].[AnnualServiceVariation]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ANNUAL SERVICE VARIATION CONSULTANT WORK'

INSERT INTO [LRSchoolV2_Dev].[dbo].[AnnualServiceVariationConsultantWork] ([Id], [AnnualServiceVariationId], [SchoolYearId], [ConsultantId], [IndividualWorkHours], [IndividualWorkHoursComment])
SELECT [Id], [AnnualServiceVariationId], [SchoolYearId], [ConsultantId], [IndividualWorkHours], [IndividualWorkHoursComment]
FROM [LRSchool].[dbo].[AnnualServiceVariationConsultantWork]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'ANNUAL SERVICE VARIATION YEARLY PRICE'

INSERT INTO [LRSchoolV2_Dev].[dbo].[AnnualServiceVariationYearlyPrice] ([Id], [AnnualServiceVariationId], [SchoolYearId], [Price], [Margin])
SELECT [Id], [AnnualServiceVariationId], [SchoolYearId], [Price], [Margin]
FROM [LRSchool].[dbo].[AnnualServiceVariationYearlyPrice]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'PERSON'

INSERT INTO [LRSchoolV2_Dev].[dbo].[Person] ([Id], [LastName], [FirstName], [BirthDate], [PhoneNumber], [Email], [AddressId], [ContactPerson1Id], [ContactPerson2Id], [BillingToContactPerson1])
SELECT [Id], [LastName], [FirstName], [BirthDate], [PhoneNumber], [Email], [AddressId], [ContactPerson1Id], [ContactPerson2Id], [BillingToContactPerson1]
FROM [LRSchool].[dbo].[Person]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'PERSON REGISTRATION'

INSERT INTO [LRSchoolV2_Dev].[dbo].[PersonRegistration] ([Id], [PersonId], [SchoolYearId], [ImageRightsGranted], [IsFullyBilled], [BilledPersonId])
SELECT [Id], [PersonId], [SchoolYearId], [ImageRightsGranted], [IsFullyBilled], [BilledPersonId]
FROM [LRSchool].[dbo].[PersonRegistration]

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'PERSON ANNUAL SERVICE REGISTRATION'

INSERT INTO [LRSchoolV2_Dev].[dbo].[PersonAnnualServiceVariation] ([Id], [PersonId], [SchoolYearId], [AnnualServiceVariationId], [PaymentsCount], [IsFullyBilled], [BilledPersonId])
SELECT [Id], [PersonId], [SchoolYearId], [AnnualServiceVariationId], [PaymentsCount], [IsFullyBilled], [BilledPersonId]
FROM [LRSchool].[dbo].[PersonAnnualServiceVariation]

---------------------------------------------------------------------------------------------------------------------------------------------

SELECT @@TRANCOUNT AS OpenTransactions

ROLLBACK TRAN
--COMMIT TRAN

SELECT @@TRANCOUNT AS OpenTransactions
