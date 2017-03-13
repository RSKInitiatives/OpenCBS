if col_length('dbo.ContractEvents','doc1') is null
alter table dbo.ContractEvents add doc1 varchar(255) null
GO

if col_length('dbo.ContractEvents','doc2') is null
alter table dbo.ContractEvents add doc2 varchar(255) null
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.10.0.0'
WHERE   [name] = 'VERSION'
GO
