if object_id('dbo.InterestWriteOffEvents') is null
begin
	create table dbo.InterestWriteOffEvents
	(
		id int not null,
		amount money not null
	)
	alter table dbo.InterestWriteOffEvents
	add constraint FK_InterestWriteOffEvents foreign key (id) references dbo.ContractEvents(id)
end
GO

if exists(select * from sys.columns 
            where Name = N'type_id' and Object_ID = Object_ID(N'city'))
begin
    alter table city 
	add default '1' for type_id
end
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.4.0.0'
WHERE   [name] = 'VERSION'
GO