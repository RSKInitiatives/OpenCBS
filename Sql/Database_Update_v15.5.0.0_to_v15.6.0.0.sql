if object_id('FK_Persons_Banks') is not null
alter table dbo.Persons drop constraint FK_Persons_Banks
GO

if object_id('FK_Persons_Banks1') is not null
alter table dbo.Persons drop constraint FK_Persons_Banks1
GO

if object_id('DF_Credit_handicapped') is not null
alter table dbo.Persons drop constraint DF_Credit_handicapped
GO

if object_id('DF_Persons_povertylevel_childreneducation') is not null
alter table dbo.Persons drop constraint DF_Persons_povertylevel_childreneducation
GO

if object_id('DF_Persons_povertylevel_socialparticipation') is not null
alter table dbo.Persons drop constraint DF_Persons_povertylevel_socialparticipation
GO

if object_id('DF_Persons_povertylevel_healthsituation') is not null
alter table dbo.Persons drop constraint DF_Persons_povertylevel_healthsituation
GO

if object_id('DF_Persons_povertylevel_economiceducation') is not null
alter table dbo.Persons drop constraint DF_Persons_povertylevel_economiceducation
GO

if exists(select * from sys.columns where Name = N'household_head' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column household_head
GO

if exists(select * from sys.columns where Name = N'nb_of_dependents' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column nb_of_dependents
GO

if exists(select * from sys.columns where Name = N'nb_of_children' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column nb_of_children
GO

if exists(select * from sys.columns where Name = N'children_basic_education' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column children_basic_education
GO

if exists(select * from sys.columns where Name = N'livestock_number' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column livestock_number
GO

if exists(select * from sys.columns where Name = N'livestock_type' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column livestock_type
GO

if exists(select * from sys.columns where Name = N'landplot_size' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column landplot_size
GO

if exists(select * from sys.columns where Name = N'home_size' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column home_size
GO

if exists(select * from sys.columns where Name = N'home_time_living_in' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column home_time_living_in
GO

if exists(select * from sys.columns where Name = N'capital_other_equipments' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column capital_other_equipments
GO

if exists(select * from sys.columns where Name = N'experience' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column experience
GO

if exists(select * from sys.columns where Name = N'nb_of_people' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column nb_of_people
GO

if exists(select * from sys.columns where Name = N'monthly_income' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column monthly_income
GO

if exists(select * from sys.columns where Name = N'monthly_expenditure' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column monthly_expenditure
GO

if exists(select * from sys.columns where Name = N'comments' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column comments
GO

if exists(select * from sys.columns where Name = N'study_level' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column study_level
GO

if exists(select * from sys.columns where Name = N'SS' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column SS
GO

if exists(select * from sys.columns where Name = N'CAF' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column CAF
GO

if exists(select * from sys.columns where Name = N'housing_situation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column housing_situation
GO

if exists(select * from sys.columns where Name = N'bank_situation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column bank_situation
GO

if exists(select * from sys.columns where Name = N'handicapped' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column handicapped
GO

if exists(select * from sys.columns where Name = N'professional_experience' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column professional_experience
GO

if exists(select * from sys.columns where Name = N'family_situation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column family_situation
GO

if exists(select * from sys.columns where Name = N'mother_name' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column mother_name
GO

if exists(select * from sys.columns where Name = N'povertylevel_childreneducation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column povertylevel_childreneducation
GO

if exists(select * from sys.columns where Name = N'povertylevel_socialparticipation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column povertylevel_socialparticipation
GO

if exists(select * from sys.columns where Name = N'povertylevel_healthsituation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column povertylevel_healthsituation
GO

if exists(select * from sys.columns where Name = N'unemployment_months' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column unemployment_months
GO

if exists(select * from sys.columns where Name = N'personalBank_id' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column personalBank_id
GO

if exists(select * from sys.columns where Name = N'businessBank_id' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column businessBank_id
GO

if exists(select * from sys.columns where Name = N'professional_situation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column professional_situation
GO

if exists(select * from sys.columns where Name = N'povertylevel_economiceducation' and Object_ID = Object_ID(N'dbo.Persons'))
alter table dbo.Persons drop column povertylevel_economiceducation
GO

delete from 
	dbo.GeneralParameters
where 
	[key] = 'ACCOUNTING_EXPORT_MODE'
	or [key] = 'ACCUMULATED_PENALTY'
	or [key] = 'BRANCH_ADDRESS'
	or [key] = 'BRANCH_CODE'
	or [key] = 'BRANCH_NAME'
	or [key] = 'CONSOLIDATION_MODE'
	or [key] = 'PENDING_REPAYMENT_MODE'
	or [key] = 'REPAYMENT_COMMENT_MANDATORY'
GO

delete from 
	dbo.GeneralParameters
where 
	[key] = 'CITY_MANDATORY'
GO

alter table dbo.Credit
alter column effective_interest_rate decimal(18,4)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.6.0.0'
WHERE   [name] = 'VERSION'
GO
