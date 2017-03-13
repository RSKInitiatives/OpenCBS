if not exists(select * from sys.columns 
            where Name = N'default_start_view' and Object_ID = Object_ID(N'Roles'))
begin
    alter table dbo.Roles
	add default_start_view varchar(20) not null default('START_PAGE')
end
GO

if not exists(select * from sys.columns 
            where Name = N'created_by' and Object_ID = Object_ID(N'Tiers'))
begin
    alter table dbo.Tiers
	add created_by int not null default((1))

alter table dbo.Tiers add constraint FK_Tiers_created_by
foreign key  (created_by) references dbo.Users(id)
end
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.7.0.0'
WHERE   [name] = 'VERSION'
GO
