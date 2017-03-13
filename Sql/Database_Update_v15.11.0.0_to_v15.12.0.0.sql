alter table dbo.Contracts
add created_by int null
GO

update
	t1
set
	t1.created_by = t2.loanofficer_id
from
	dbo.Contracts t1
inner join
	dbo.Credit t2 on t2.id = t1.id
GO

alter table dbo.Contracts
alter column created_by int not null
GO

alter table dbo.Contracts
add constraint FK_Contracts_created_by foreign key (created_by) references dbo.Users (id)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.12.0.0'
WHERE   [name] = 'VERSION'
GO
