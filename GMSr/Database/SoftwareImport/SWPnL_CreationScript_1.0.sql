USE [xxx]
GO
/****** Object:  StoredProcedure [dbo].[sp_SWPnL_ImportProductsAndLicenses]    Script Date: 03/21/2011 12:54:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Jeanquart
-- Create date: 2011-03-21
-- Description:	Imports products and licenses in SWPnL_Import
-- =============================================
CREATE PROCEDURE [dbo].[sp_SWPnL_ImportProductsAndLicenses] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM SWPnL_Import
INSERT INTO SWPnL_Import
SELECT 
ISNULL(RMT_PRO_CD, '')+ '___' + ISNULL(RMT_PRO_DESC, '') + '___' +ISNULL(RMT_TECHNIQUE_REM, '') as ProductSoftwareName, 
NULL as ProductVersion,
NULL as ProductCompany,
RMT_VEN_NM as ProductOther,
NULL as ProductSupportedUntil,
RMT_FIN_MAINT_DT as LicenseMaintenanceEndDate,
RMT_DEBUT_MAINT_DT as LicenseMaintenanceStartDate,
NULL as ProdutComment,
'ABAC' as SourceName,
'ABAC.' + CONVERT(varchar(max), RMT_MAT_ID) as UniqueLicenseIdentifier,
RMT_SERIE_NO as LicenseSerial,
NULL as LicenseComment,
RMT_USERID_CD AS UserName,
NULL as ComputerName, 
HASHBYTES('SHA1', LOWER('ABAC.' + CONVERT(nvarchar(max), RMT_MAT_ID))) As LicenseHash,
HASHBYTES('SHA1', CONVERT(nvarchar(max),LOWER(ISNULL(RMT_PRO_CD, '')+ '___' + ISNULL(RMT_PRO_DESC, '') + '___' +ISNULL(RMT_TECHNIQUE_REM, '') + '.Unknown.Unknown.ABAC' ))) as ProductHash
FROM [s-olaf-prod70].GMS.dbo.Syslog_materiel_view
WHERE 
LOWER(RMT_PARENT_CTG_NM) LIKE '%logiciel%'
OR 
LOWER(RMT_PARENT_CTG_NM) LIKE '%software%'
OR 
LOWER(RMT_CTG_NM) LIKE '%software%'
OR 
LOWER(RMT_CTG_NM) LIKE '%logiciel%'
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SWPnL_ProcessImports]    Script Date: 03/21/2011 12:55:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Eric Jeanquart
-- Create date: 2011-03-21
-- Description:	Processes the imported software and licenses
-- =============================================
CREATE PROCEDURE [dbo].[sp_SWPnL_ProcessImports] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--New products to R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'New imported software product', 
'<Name>' + Imports.ProductSoftwareName  
+ '</Name><Version>' + ISNULL(Imports.ProductVersion, 'NULL') 
+ '</Version><CompanyName>' + ISNULL(Imports.ProductCompany, 'NULL') 
+ '</CompanyName><Other>' + ISNULL(Imports.ProductOther, 'NULL') 
+ '</Other><SupportedUntil>' + ISNULL(CONVERT(nvarchar(50),Imports.ProductSupportedUntil), 'NULL') 
+ '</SupportedUntil><Comment>' + ISNULL(Imports.ProductComment, 'NULL') 
+ '</Comment><Source>' + Imports.SourceName + '</Source>',
Imports.SourceName
FROM SWPnL_Import Imports 
WHERE
Imports.ProductHash NOT IN 
(SELECT HASHBYTES('SHA1', LOWER(Products.Name + '.' + ISNULL(Products.Version, 'Unknown') + '.' + ISNULL(Products.CompanyName, 'Unknown') + '.' + ISNULL(Products.Source, 'Local') )) FROM R_SoftwareProduct Products WHERE Products.Source <> 'Local')
GROUP BY Imports.ProductSoftwareName, Imports.ProductVersion, Imports.ProductCompany, Imports.ProductOther, Imports.ProductSupportedUntil, Imports.ProductComment, Imports.SourceName


--New products to R_SoftwareProduct table
INSERT INTO R_SoftwareProduct ([Name], [Version], CompanyName, Other, SoftwareProductStatusId, SupportedUntil, Comment, Source)
SELECT 
Imports.ProductSoftwareName,
Imports.ProductVersion,
Imports.ProductCompany,
Imports.ProductOther,
9,
Imports.ProductSupportedUntil,
Imports.ProductComment,
Imports.SourceName
FROM SWPnL_Import Imports 
WHERE
Imports.ProductHash NOT IN 
(SELECT HASHBYTES('SHA1', LOWER(Products.Name + '.' + ISNULL(Products.Version, 'Unknown') + '.' + ISNULL(Products.CompanyName, 'Unknown') + '.' + ISNULL(Products.Source, 'Local') )) FROM R_SoftwareProduct Products WHERE Products.Source <> 'Local')
GROUP BY Imports.ProductSoftwareName, Imports.ProductVersion, Imports.ProductCompany, Imports.ProductOther, Imports.ProductSupportedUntil, Imports.ProductComment, Imports.SourceName


/*
Changes to products are not reported to the R_ImportHistory table because they shouldn't change.
External products that are absent from the import are ignored.
*/

--Add new orders to the R_ImportHistory table
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT  
GETDATE(),
'New imported software order',
'<ContractOrderId>NULL</ContractOrderId><SoftwareProductId>' + CONVERT(nvarchar(max),Products.Id) + '</SoftwareProductId><PreviousSoftwareOrderId>NULL</PreviousSoftwareOrderId><OlafRef> ' + 'External item: ' + Products.Source + '</OlafRef><Vendor>NULL</Vendor><Comment>NULL</Comment>',
Products.Source
FROM R_SoftwareProduct Products 
WHERE Products.Id NOT IN (SELECT SoftwareProductId FROM R_SoftwareOrder)
AND Products.Source <> 'Local'

--Add new orders to the R_SoftwareOrder table
INSERT INTO R_SoftwareOrder (OlafRef, SoftwareProductId)
SELECT  
'External item: ' + Products.Source,
Products.Id
FROM R_SoftwareProduct Products 
WHERE Products.Id NOT IN (SELECT SoftwareProductId FROM R_SoftwareOrder)
AND Products.Source <> 'Local'

/*
Changes to orders are not reported to the R_ImportHistory table because they shouldn't change.
External orders that are absent from the import are ignored.
*/

--Add new licences to R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'New imported software license',
'<SoftwareOrderId>' + CONVERT(nvarchar(max), Orders.Id) + '</SoftwareOrderId>'
+
'<SoftwareLicenseTypeId>NULL</SoftwareLicenseTypeId>'
+
'<MultiUserQuantity>NULL</MultiUserQuantity>'
+
'<PreviousSoftwareLicenseId>NULL</PreviousSoftwareLicenseId>'
+
'<DateStart>' + ISNULL(CONVERT(nvarchar(max), Imports.LicenseMaintenanceStartDate ), 'NULL') + '</DateStart>'
+
'<DateEnd>' + ISNULL(CONVERT(nvarchar(max), Imports.LicenseMaintenanceEndDate ), 'NULL') + '</DateStart>'
+
'<SerialKey>' + ISNULL(Imports.LicenseSerial, 'NULL') + '</SerialKey>' 
+
'<Comment>' + ISNULL(Imports.LicenseComment, 'NULL') + '</Comment>'
+
'<IsPresentInSource>True</IsPresentInSource>'
+
'<SourceId>' + Imports.UniqueLicenseIdentifier + '</SourceId>',
Imports.SourceName
FROM SWPnL_Import Imports
INNER JOIN R_SoftwareProduct Products ON 
HASHBYTES('SHA1', LOWER(Products.Name + '.' + ISNULL(Products.Version, 'Unknown') + '.' + ISNULL(Products.CompanyName, 'Unknown') + '.' + ISNULL(Products.Source, 'Local') ))
=
Imports.ProductHash
INNER JOIN R_SoftwareOrder Orders ON Orders.SoftwareProductId = Products.Id
WHERE Products.Source <> 'Local'
AND Imports.UniqueLicenseIdentifier NOT IN (SELECT SourceId FROM R_SoftwareLicense)

--Add new licenses to R_SoftwareLicenses
INSERT INTO R_SoftwareLicense (SoftwareOrderId, DateStart, DateEnd, SerialKey, Comment, IsPresentInSource, SourceId)
SELECT 
Orders.Id,
Imports.LicenseMaintenanceStartDate,
Imports.LicenseMaintenanceEndDate,
Imports.LicenseSerial,
Imports.LicenseComment,
1,
Imports.UniqueLicenseIdentifier
FROM SWPnL_Import Imports
INNER JOIN R_SoftwareProduct Products ON 
HASHBYTES('SHA1', LOWER(Products.Name + '.' + ISNULL(Products.Version, 'Unknown') + '.' + ISNULL(Products.CompanyName, 'Unknown') + '.' + ISNULL(Products.Source, 'Local') ))
=
Imports.ProductHash
INNER JOIN R_SoftwareOrder Orders ON Orders.SoftwareProductId = Products.Id
WHERE Products.Source <> 'Local'
AND Imports.UniqueLicenseIdentifier NOT IN (SELECT SourceId FROM R_SoftwareLicense)


--Insert licenses that are not in the source anymore into R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'License is not available in the source',
'<Id>' + ISNULL(CONVERT(nvarchar(max),Licenses.Id), 'NULL') + '</Id>'
+
'<SoftwareOrderId>' + ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareOrderId), 'NULL') + '</SoftwareOrderId>'
+
'<SoftwareLicenseTypeId>'+ ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareLicenseTypeId), 'NULL') +  '</SoftwareLicenseTypeId>'
+
'<MultiUserQuantity>' + ISNULL(CONVERT(nvarchar(max),Licenses.MultiUserQuantity), 'NULL') + '</MultiUserQuantity>'
+
'<PreviousSoftwareLicenseId>' + ISNULL(CONVERT(nvarchar(max),Licenses.PreviousSoftwareLicenseId), 'NULL') + '</PreviousSoftwareLicenseId>'
+
'<DateStart>' + ISNULL(CONVERT(nvarchar(max), Licenses.DateStart ), 'NULL') + '</DateStart>'
+
'<DateEnd>' + ISNULL(CONVERT(nvarchar(max), Licenses.DateEnd ), 'NULL') + '</DateStart>'
+
'<SerialKey>' + ISNULL(Licenses.SerialKey, 'NULL') + '</SerialKey>' 
+
'<Comment>' + ISNULL(Licenses.Comment, 'NULL') + '</Comment>'
+
'<IsPresentInSource>False</IsPresentInSource>'
+
'<SourceId>' + Licenses.SourceId + '</SourceId>',
Imports.SourceName
FROM R_SoftwareLicense Licenses
LEFT JOIN SWPnL_Import Imports ON LOWER(Imports.UniqueLicenseIdentifier) = LOWER(Licenses.SourceId)
WHERE Imports.UniqueLicenseIdentifier IS NULL
AND Licenses.SourceId NOT LIKE 'Local%'
AND Licenses.IsPresentInSource = 1



--Update Source status of licenses that are not in the source anymore
UPDATE R_SoftwareLicense SET IsPresentInSource = 0 
WHERE SourceId NOT IN (SELECT UniqueLicenseIdentifier FROM SWPnL_Import)
AND SourceId NOT LIKE 'Local%'
AND IsPresentInSource = 1

--Insert licenses with different data in R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'License info changed',
'<SoftwareOrderId>' + ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareOrderId), 'NULL') + '</SoftwareOrderId>'
+
'<SoftwareLicenseTypeId>'+ ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareLicenseTypeId), 'NULL') +  '</SoftwareLicenseTypeId>'
+
'<MultiUserQuantity>' + ISNULL(CONVERT(nvarchar(max),Licenses.MultiUserQuantity), 'NULL') + '</MultiUserQuantity>'
+
'<PreviousSoftwareLicenseId>' + ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareOrderId), 'NULL') + '</PreviousSoftwareLicenseId>'
+
'<DateStart>' + ISNULL(CONVERT(nvarchar(max), Imports.LicenseMaintenanceStartDate ), 'NULL') + '</DateStart>'
+
'<DateEnd>' + ISNULL(CONVERT(nvarchar(max), Imports.LicenseMaintenanceEndDate ), 'NULL') + '</DateStart>'
+
'<SerialKey>' + ISNULL(Imports.LicenseSerial, 'NULL') + '</SerialKey>' 
+
'<Comment>' + ISNULL(Imports.LicenseComment, 'NULL') + '</Comment>'
+
'<IsPresentInSource>True</IsPresentInSource>'
+
'<SourceId>' + Imports.UniqueLicenseIdentifier + '</SourceId>',
Imports.SourceName
FROM SWPnL_Import Imports
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
WHERE SourceId NOT LIKE 'Local%'
AND
(ISNULL(Imports.LicenseMaintenanceStartDate, '1900-01-01') <> ISNULL(Licenses.DateStart, '1900-01-01') 
	OR
	ISNULL(Imports.LicenseMaintenanceEndDate, '1900-01-01') <> ISNULL(Licenses.DateEnd, '1900-01-01')
	OR
	ISNULL(Imports.LicenseSerial, 'NULL') NOT LIKE ISNULL(Licenses.SerialKey, 'NULL')
	OR
	ISNULL(Imports.LicenseComment, 'NULL') NOT LIKE ISNULL(Licenses.Comment, 'NULL'))

--Update the licenses with different data in R_SoftwareLicense

UPDATE R_SoftwareLicense 
SET DateStart =  LicenseMaintenanceStartDate,
DateEnd = LicenseMaintenanceEndDate,
SerialKey = LicenseSerial,
Comment = LicenseComment 
FROM SWPnL_Import Imports
WHERE R_SoftwareLicense.SourceId = UniqueLicenseIdentifier
AND
SourceId NOT LIKE 'Local%'
AND
(ISNULL(Imports.LicenseMaintenanceStartDate, '1900-01-01') <> ISNULL(R_SoftwareLicense.DateStart, '1900-01-01') 
	OR
	ISNULL(Imports.LicenseMaintenanceEndDate, '1900-01-01') <> ISNULL(R_SoftwareLicense.DateEnd, '1900-01-01')
	OR
	ISNULL(Imports.LicenseSerial, 'NULL') NOT LIKE ISNULL(R_SoftwareLicense.SerialKey, 'NULL')
	OR
	ISNULL(Imports.LicenseComment, 'NULL') NOT LIKE ISNULL(R_SoftwareLicense.Comment, 'NULL'))

--Insert licenses that have been reactivated in the source into R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'License is in the source again',
'<SoftwareOrderId>' + ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareOrderId), 'NULL') + '</SoftwareOrderId>'
+
'<SoftwareLicenseTypeId>'+ ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareLicenseTypeId), 'NULL') +  '</SoftwareLicenseTypeId>'
+
'<MultiUserQuantity>' + ISNULL(CONVERT(nvarchar(max),Licenses.MultiUserQuantity), 'NULL') + '</MultiUserQuantity>'
+
'<PreviousSoftwareLicenseId>' + ISNULL(CONVERT(nvarchar(max),Licenses.SoftwareOrderId), 'NULL') + '</PreviousSoftwareLicenseId>'
+
'<DateStart>' + ISNULL(CONVERT(nvarchar(max), Licenses.DateStart), 'NULL') + '</DateStart>'
+
'<DateEnd>' + ISNULL(CONVERT(nvarchar(max), Licenses.DateEnd), 'NULL') + '</DateStart>'
+
'<SerialKey>' + ISNULL(Licenses.SerialKey, 'NULL') + '</SerialKey>' 
+
'<Comment>' + ISNULL(Licenses.Comment, 'NULL') + '</Comment>'
+
'<IsPresentInSource>True</IsPresentInSource>'
+
'<SourceId>' + Licenses.SourceId + '</SourceId>',
Imports.SourceName
FROM R_SoftwareLicense Licenses 
INNER JOIN SWPnL_Import Imports
ON Imports.UniqueLicenseIdentifier = Licenses.SourceId
WHERE 
Licenses.IsPresentInSource = 0
AND Licenses.SourceId IN (SELECT UniqueLicenseIdentifier FROM SWPnL_Import)

--Update licenses that have been reactivated in the source in R_SoftwareLicense
UPDATE R_SoftwareLicense SET IsPresentInSource = 1 
WHERE SourceId IN (SELECT UniqueLicenseIdentifier FROM SWPnL_Import)
AND SourceId NOT LIKE 'Local%'
AND IsPresentInSource = 0

-- Add new unknown hardware to the R_UnknownHardware table
INSERT INTO R_UnknownHardware
SELECT ImportHardware.ComputerName, GETDATE(),ImportHardware.SourceName FROM (
SELECT ComputerName, SourceName FROM SWPnL_Import 
GROUP BY ComputerName, SourceName) As ImportHardware
WHERE 
(LOWER(ImportHardware.ComputerName) NOT IN (SELECT LOWER(OlafHardName) FROM Olaf_Hard WHERE OlafHardName IS NOT NULL)) 
AND
ImportHardware.ComputerName + '.' + ImportHardware.SourceName NOT IN (SELECT MachineName + '.' + SourceName FROM R_UnknownHardware)
	
-- Add new unknown users to the R_UnknownUser table
INSERT INTO R_UnknownUser
SELECT ImportUser.UserName, GETDATE(),ImportUser.SourceName FROM (
SELECT UserName, SourceName FROM SWPnL_Import 
GROUP BY UserName, SourceName) As ImportUser
WHERE 
(LOWER(ImportUser.UserName) NOT IN (SELECT LOWER(PEO_USERID_CD) FROM Olaf_User WHERE PEO_USERID_CD IS NOT NULL)) 
AND
ImportUser.UserName + '.' + ImportUser.SourceName NOT IN (SELECT UserName + '.' + SourceName FROM R_UnknownUser)

--Add new license assignements to the R_ImportHistory table
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT 
GETDATE(),
'New license assignement',
'<SoftwareLicenseId>' + CONVERT(nvarchar(max), Licenses.Id) + '</SoftwareLicenseId>'
+
'<OlafUserId>' + ISNULL(CONVERT(nvarchar(max), Users.OlafUserId), 'NULL') + '</OlafuserId>'
+
'<HardwareItemId>' + ISNULL(CONVERT(nvarchar(max), Hard.OlafHardId), 'NULL') + '</HardwareItemId>'
+
'<DateAdded>' + CONVERT(nvarchar(max), GETDATE())+ '</DateAdded>'
+
'<Comment>NULL</Comment>',
Imports.SourceName
FROM SWPnL_Import Imports
LEFT JOIN Olaf_Hard Hard ON LOWER(Hard.OlafHardName) = LOWER(Imports.ComputerName) 
LEFT JOIN Olaf_User Users ON LOWER(Users.PEO_USERID_CD) = LOWER(Imports.UserName)
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
LEFT JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
WHERE 
ISNULL(Imports.ComputerName, '') NOT IN (SELECT MachineName FROM R_UnknownHardware)
AND ISNULL(Imports.Username,'') NOT IN (SELECT UserName FROM R_UnknownUser)
AND (Hard.OlafHardName IS NOT NULL OR Users.PEO_USERID_CD IS NOT NULL)
AND InUse.Id IS NULL

--Add new license assignements to the R_SoftwareLicenceInUse
INSERT INTO R_SoftwareLicenseInUse (SoftwareLicenseId, OlafuserId, HardwareItemId, DateAdded, Comment)
SELECT 
Licenses.Id,
Users.OlafUserId,
Hard.OlafHardId,
GETDATE(),
NULL
FROM SWPnL_Import Imports
LEFT JOIN Olaf_Hard Hard ON LOWER(Hard.OlafHardName) = LOWER(Imports.ComputerName) 
LEFT JOIN Olaf_User Users ON LOWER(Users.PEO_USERID_CD) = LOWER(Imports.UserName)
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
LEFT JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
WHERE 
ISNULL(Imports.ComputerName, '') NOT IN (SELECT MachineName FROM R_UnknownHardware)
AND ISNULL(Imports.Username,'') NOT IN (SELECT UserName FROM R_UnknownUser)
AND (Hard.OlafHardName IS NOT NULL OR Users.PEO_USERID_CD IS NOT NULL)
AND InUse.Id IS NULL

--Add license assignements removal to R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT
GETDATE(),
'License is not assigned anymore (old value)',
'<Id>' + ISNULL(CONVERT(nvarchar(max),InUse.Id), 'NULL') + '</Id>'
+
'<SoftwareLicenseId>'+ ISNULL(CONVERT(nvarchar(max),InUse.SoftwareLicenseId), 'NULL') +  '</SoftwareLicenseId>'
+
'<OlafUserId>' + ISNULL(CONVERT(nvarchar(max),InUse.OlafUserId), 'NULL') + '</OlafUserId>'
+
'<HardwareItemId>' + ISNULL(CONVERT(nvarchar(max),InUse.HardwareItemId), 'NULL') + '</HardwareItemId>'
+
'<DateAdded>' + ISNULL(CONVERT(nvarchar(max), InUse.DateAdded), 'NULL') + '</DateAdded>'
+
'<Comment>' + ISNULL(CONVERT(nvarchar(max), InUse.Comment), 'NULL') + '</Comment>'
,
Imports.SourceName
FROM SWPnL_Import Imports
INNER JOIN R_SoftwareLicense Licenses ON LOWER(Licenses.SourceId) = LOWER(Imports.UniqueLicenseIdentifier)
INNER JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
WHERE Imports.UserName IS NULL AND Imports.ComputerName IS NULL


--Delete removed license assignements from R_SoftwareLicenseInUse
DELETE FROM R_SoftwareLicenseInUse 
WHERE
R_SoftwareLicenseInUse.Id IN 
(SELECT
InUse.Id
FROM SWPnL_Import Imports
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
INNER JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
WHERE Imports.UserName IS NULL AND Imports.ComputerName IS NULL)

--Add updated license assignements to R_ImportHistory
INSERT INTO R_ImportHistory (ChangeDate, ChangeType, NewValue, SourceName)
SELECT
GETDATE(),
'Change to license assignement',
'<Id>' + ISNULL(CONVERT(nvarchar(max),InUse.Id), 'NULL') + '</Id>'
+
'<SoftwareLicenseId>'+ ISNULL(CONVERT(nvarchar(max),InUse.SoftwareLicenseId), 'NULL') +  '</SoftwareLicenseId>'
+
'<OlafUserId>' + ISNULL(CONVERT(nvarchar(max),Users.OlafUserId), 'NULL') + '</OlafUserId>'
+
'<HardwareItemId>' + ISNULL(CONVERT(nvarchar(max),Hard.OlafHardId), 'NULL') + '</HardwareItemId>'
+
'<DateAdded>' + ISNULL(CONVERT(nvarchar(max), GETDATE()), 'NULL') + '</DateAdded>'
+
'<Comment>' + ISNULL(CONVERT(nvarchar(max), InUse.Comment), 'NULL') + '</Comment>'
,
Imports.SourceName
FROM 
SWPnL_Import Imports
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
INNER JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
LEFT JOIN Olaf_User Users ON LOWER(Users.PEO_USERID_CD) = LOWER(Imports.Username)
LEFT JOIN Olaf_Hard Hard ON LOWER(Hard.OlafHardName) = LOWER(Imports.ComputerName)
WHERE (Imports.UserName IS NOT NULL OR Imports.ComputerName IS NOT NULL)
AND
(
ISNULL(CONVERT(nvarchar(max),InUse.OlafUserId), 'NULL') NOT LIKE ISNULL(CONVERT(nvarchar(max),Users.OlafUserId), 'NULL')
OR 
ISNULL(CONVERT(nvarchar(max),InUse.HardwareItemId), 'NULL') NOT LIKE ISNULL(CONVERT(nvarchar(max),Hard.OlafHardId), 'NULL'))

-- Update license assignements in R_SoftwareLicenseInUse
UPDATE InUseToUpdate
SET InUseToUpdate.OlafUserId = Updates.UserId,
InUseToUpdate.HardwareItemId = Updates.HardId
FROM
R_SoftwareLicenseInUse InUseToUpdate
INNER JOIN 
(SELECT InUse.Id as InUseId, Users.OlafUserId as UserId, Hard.OlafHardId as HardId FROM
SWPnL_Import Imports
INNER JOIN R_SoftwareLicense Licenses ON Licenses.SourceId = Imports.UniqueLicenseIdentifier
INNER JOIN R_SoftwareLicenseInUse InUse ON InUse.SoftwareLicenseId = Licenses.Id
LEFT JOIN Olaf_User Users ON LOWER(Users.PEO_USERID_CD) = LOWER(Imports.Username)
LEFT JOIN Olaf_Hard Hard ON LOWER(Hard.OlafHardName) = LOWER(Imports.ComputerName)
WHERE (Imports.UserName IS NOT NULL OR Imports.ComputerName IS NOT NULL)
AND
(
ISNULL(CONVERT(nvarchar(max),InUse.OlafUserId), 'NULL') NOT LIKE ISNULL(CONVERT(nvarchar(max),Users.OlafUserId), 'NULL')
OR 
ISNULL(CONVERT(nvarchar(max),InUse.HardwareItemId), 'NULL') NOT LIKE ISNULL(CONVERT(nvarchar(max),Hard.OlafHardId), 'NULL'))) Updates 
ON Updates.InUseId = InUseToUpdate.Id
END
