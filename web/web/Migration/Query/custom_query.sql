
use web_test
select FiscalYearId as [Key],
FiscalYearName as [Value] 
from dbo.[FiscalYear]
where Status=1

GO
ALTER TABLE dbo.[FiscalYear]
drop column IsActive
GO


update dbo.[FiscalYear] set
IsCurrent=1 where FiscalYearId=
(select top 1 FiscalYearId from 
dbo.FiscalYear order by EndDateAD desc)

select * from dbo.Member


--use web_test
select ProvinceId as [Key],
ProvinceName as [Value] 
from dbo.[Province]
where [Status]=1

declare @ProvinceId int
set @ProvinceId=0
select DistrictId as [Key],
DistrictName as [Value] 
from dbo.[District]
where [Status]=1 and 
(ProvinceId=@ProvinceId or isnull(@ProvinceId,0)=0)


select Id as [Key],
[Name] as [Value] 
from dbo.[Occupation]
where [Status]=1


select ShareTypeId as [Key],
ShareTypeName as [Value] 
from dbo.[ShareTypes]
where [Status]=1