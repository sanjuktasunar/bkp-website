
GO
ALTER TABLE [dbo].[ShareTypes]
ADD PricePerKitta money null
GO

GO
DROP VIEW [dbo].[ShareTypeView]
GO

GO
CREATE VIEW [dbo].[ShareTypeView]
AS
SELECT A.*
,B.FiscalYearName
FROM ShareTypes AS A
JOIN FiscalYear AS B ON B.FiscalYearId=A.FiscalYearId
GO