alter table SavingContracts add loan_id int
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.15.0.0'
WHERE   [name] = 'VERSION'
GO