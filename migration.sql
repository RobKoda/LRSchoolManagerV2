USE [master]
GO

BEGIN TRAN
GO

---------------------------------------------------------------------------------------------------------------------------------------------

DELETE FROM [LRSchoolV2_Dev].[dbo].[SchoolYear]
GO

---------------------------------------------------------------------------------------------------------------------------------------------

PRINT 'SCHOOL YEARS'

INSERT INTO [LRSchoolV2_Dev].[dbo].[SchoolYear]
SELECT [Id], [StartDate], [EndDate], [MembershipFee]
FROM [LRSchool].[dbo].[SchoolYear]

---------------------------------------------------------------------------------------------------------------------------------------------

SELECT @@TRANCOUNT AS OpenTransactions

ROLLBACK TRAN
--COMMIT TRAN

SELECT @@TRANCOUNT AS OpenTransactions
