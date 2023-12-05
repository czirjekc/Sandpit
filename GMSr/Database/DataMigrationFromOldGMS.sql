INSERT INTO Contract_Order (OrderName, Title)

SELECT DISTINCT SoftContract_CS, 'InsertedForGMSR' FROM SoftInv WHERE SoftContract_CS NOT IN (SELECT OrderName FROM Contract_Order)

 

INSERT INTO R_SoftwareProduct (Name, [Version], CompanyName, SoftwareProductStatusId, Comment, Other)

SELECT 

LTRIM(ISNULL(Company.SoftListCompanyName, '') + ' ' + ISNULL(SoftList.SoftListName, '_')) Name, 

RTRIM(ISNULL(SoftInv.SoftName, '_') + ' ' + ISNULL(SoftInv.SoftVersion, '')) [Version], 

ISNULL(Company.SoftListCompanyName, 'NoCompanyName') CompanyName, 

ISNULL(SoftList.SoftListStatus, 9) SoftwareProductStatusId,

LTRIM(ISNULL(SoftList.SoftListDesc,'') + ' ' + ISNULL(SoftInv.SoftInvExeName,'')) Comment,

SoftInv.SoftInvId

FROM SoftList

LEFT JOIN [SoftList-Company] Company ON Company.SoftListCompanyId = CONVERT(int,SoftListCompany)

LEFT JOIN SoftInv ON SoftInv.SoftListId = SoftList.SoftListId

WHERE LTRIM(SoftList.SoftListName) <> ''

AND LTRIM(SoftInv.SoftName) <> ''

 

INSERT INTO R_SoftwareOrder (SoftwareProductId, ContractOrderId, Comment)

SELECT R_SoftwareProduct.Id, 

Contract_Order.ContractOrderId, 

'SoftContract_CS: ' + Isnull(SoftContract_CS, ' - ')+ ' SoftContract_Digit: ' + ISNULL(SoftContract_Digit, ' - ') + ' SoftOrder_Nr: ' + ISNULL(SoftOrder_Nr, ' -') FROM R_SoftwareProduct

LEFT JOIN SoftInv ON SoftInv.SoftInvId = R_SoftwareProduct.Other

LEFT JOIN Contract_Order ON Contract_Order.OrderName = SoftInv.SoftContract_CS

INSERT INTO R_OrderFormDocument ([Path], ContractOrderId)

SELECT SoftDocPath, Contract_Order.ContractOrderId FROM SoftInv

INNER JOIN Contract_Order ON Contract_Order.Ordername = SoftInv.Softcontract_cs

WHERE SoftDocPath IS NOT NULL

 

declare @idSoftwareOrder int

declare @LicenceModel int

declare @SoftInvMaintDateIn datetime

declare @SoftInvMaintDateOut datetime

declare @SoftInvId int

declare @LicenceNumber int

declare @SoftCodeCounter int

declare id_call_cur cursor for

select R_SoftwareOrder.Id, SoftInv.SoftLicenceModel, SoftInv.SoftInvMaintDateIn, SoftInv.SoftInvMaintDateOut, SoftInv.SoftInvId, SoftInv.SoftLicenceNumber 

from R_SoftwareOrder 

INNER JOIN R_SoftwareProduct ON R_SoftwareProduct.id = R_SoftwareOrder.SoftwareProductId 

INNER JOIN SoftInv ON SoftInv.SoftInvId = R_SoftwareProduct.Other

open id_call_cur 

fetch id_call_cur into @idSoftwareOrder, @LicenceModel, @SoftInvMaintDateIn, @SoftInvMaintDateOut, @SoftInvId, @LicenceNumber

while @@fetch_status=0

begin

Select @SoftCodeCounter = count(*) FROM SoftCode WHERE SoftCode.SoftInvId = @SoftInvId

If @SoftCodeCounter >= ISNULL(@LicenceNumber, 0)

Begin

INSERT INTO R_SoftwareLicense (SoftwareOrderId, SoftwarelicenseTypeId, DateStart, DateEnd, SerialKey, Comment)

SELECT @idSoftwareOrder, ISNULL(@LicenceModel, 1), @SoftInvMaintDateIn,@SoftInvMaintDateOut, RTRIM(LTRIM(ISNULL(SoftCode1,' ') + ISNULL(SoftCode2,' ') + ISNULL(SoftCode3,' '))), RTRIM(LTRIM(ISNULL(SoftCodeUserId,' ') + ISNULL(CONVERT(nvarchar(max),SoftCodeDateAttr),' ') + ISNULL(SoftCodeStatus,' '))) 

FROM SoftCode WHERE SoftCode.SoftInvId = @SoftInvId


End

If @SoftCodeCounter < ISNULL(@LicenceNumber, 0)

Begin

INSERT INTO R_SoftwareLicense (SoftwareOrderId, SoftwarelicenseTypeId, DateStart, DateEnd, SerialKey, Comment)

SELECT @idSoftwareOrder, ISNULL(@LicenceModel, 1), @SoftInvMaintDateIn,@SoftInvMaintDateOut, RTRIM(LTRIM(ISNULL(SoftCode1,' ') + ISNULL(SoftCode2,' ') + ISNULL(SoftCode3,' '))), RTRIM(LTRIM(ISNULL(SoftCodeUserId,' ') + ISNULL(CONVERT(nvarchar(max),SoftCodeDateAttr),' ') + ISNULL(SoftCodeStatus,' '))) 

FROM SoftCode WHERE SoftCode.SoftInvId = @SoftInvId


While @SoftCodeCounter < ISNULL(@LicenceNumber, 0)

Begin

INSERT INTO R_SoftwareLicense (SoftwareOrderId, SoftwarelicenseTypeId, DateStart, DateEnd, SerialKey, Comment)

SELECT @idSoftwareOrder, ISNULL(@LicenceModel, 1),@SoftInvMaintDateIn,@SoftInvMaintDateOut, '', ''


SELECT @SoftCodeCounter = (@SoftCodeCounter + 1) 

End

End

fetch id_call_cur into @idSoftwareOrder, @LicenceModel, @SoftInvMaintDateIn, @SoftInvMaintDateOut, @SoftInvId, @LicenceNumber

end

close id_call_cur

deallocate id_call_cur

