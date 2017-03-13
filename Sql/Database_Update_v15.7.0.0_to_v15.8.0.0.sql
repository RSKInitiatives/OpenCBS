update
    Installments
set
    start_date = t1.date
from
    Installments i
left join
(
    select
        contract_id
        , expected_date date
        , number n
    from
        Installments
) t1 on t1.contract_id = i.contract_id and i.number - 1 = t1.n
where
    i.start_date != t1.date
GO

update
    InstallmentHistory
set
    start_date = t1.date
from
    InstallmentHistory i
left join
(
    select
        contract_id
        , event_id
        , expected_date date
        , number n
    from
        InstallmentHistory
) t1 on t1.contract_id = i.contract_id and i.number - 1 = t1.n and t1.event_id = i.event_id
where
    i.start_date != t1.date
GO


UPDATE  [TechnicalParameters]
SET     [value] = 'v15.8.0.0'
WHERE   [name] = 'VERSION'
GO
