IF not exists (Select * from dbo.GeneralParameters WHERE [key] = 'SHOW_EXTRA_INTEREST_COLUMN')
BEGIN
INSERT INTO [GeneralParameters]([key], [value]) VALUES('SHOW_EXTRA_INTEREST_COLUMN', 0)
END
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.13.0.0'
WHERE   [name] = 'VERSION'
GO