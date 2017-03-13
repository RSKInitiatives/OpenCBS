alter table dbo.Persons
add uuid uniqueidentifier not null default(newid())

alter table dbo.Branches
add is_default bit not null default(0)

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.16.0.0'
WHERE   [name] = 'VERSION'
GO