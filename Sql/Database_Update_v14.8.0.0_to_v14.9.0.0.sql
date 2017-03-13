UPDATE Credit 
SET interest_rate = round(interest_rate,2)
GO

UPDATE Packages
SET interest_rate = round(interest_rate,2),
	interest_rate_min = round(interest_rate_min,2),
	interest_rate_max = round(interest_rate_max,2)
GO

UPDATE ReschedulingOfALoanEvents
SET previous_interest_rate = round(previous_interest_rate,2),
	interest = round(interest,2)
GO

UPDATE TrancheEvents
SET interest_rate = round(interest_rate,2)
GO

alter table RepaymentEvents
add bounce_fee money not null default(0)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.9.0.0'
WHERE   [name] = 'VERSION'
GO