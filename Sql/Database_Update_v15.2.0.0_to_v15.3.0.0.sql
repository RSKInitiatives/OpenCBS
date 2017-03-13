alter table dbo.Credit
add schedule_type int, script_name nvarchar(255) null
GO

alter table dbo.Credit
alter column effective_interest_rate decimal(18,4)
GO

update
	t1
set
	t1.schedule_type = t2.loan_type,
	t1.script_name = t2.script_name
from
	dbo.Credit t1
left join
	dbo.Packages t2 on t2.id = t1.package_id
GO

create table 
	LoanPurpose
	(
		id int identity(1,1) not null,
		name nvarchar(255) not null,
		parent_id int null,
		deleted bit not null,
		constraint PK_LoanPurpose primary key clustered
		(
			[id] asc
		) with (pad_index  = off, statistics_norecompute = off, ignore_dup_key = off, allow_row_locks = on, allow_page_locks = on) on [PRIMARY]
	) on [PRIMARY]
GO

set identity_insert LoanPurpose on 
insert into
	LoanPurpose (id, name, parent_id, deleted)
select 
	id, name, parent_id, deleted
from
	EconomicActivities
set identity_insert LoanPurpose off
GO

alter table 
	contracts
drop constraint 
	FK_Contracts_EconomicActivities
GO

alter table 
	contracts
add constraint 
	FK_Contracts_LoanPurpose
foreign key 
	(activity_id)
references 
	loanpurpose(id)
GO

if object_id('dbo.PenaltyWriteOffEvents') is null
begin
	create table dbo.PenaltyWriteOffEvents
	(
		id int not null,
		amount money not null
	)

	alter table dbo.PenaltyWriteOffEvents
	add constraint FK_PenaltyWriteOffEvents foreign key (id) references dbo.ContractEvents(id)
end
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.3.0.0'
WHERE   [name] = 'VERSION'
GO