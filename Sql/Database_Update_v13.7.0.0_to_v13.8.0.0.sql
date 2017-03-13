
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LPAE','Loan Penalty Accrual Event',730)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('AILE','Accrual Interest Loan Event',740)
GO

CREATE TABLE LoanPenaltyAccrualEvents
(
	id INT NOT NULL,
	penalty MONEY NOT NULL,
)
GO

CREATE TABLE AccrualInterestLoanEvents
(
	id INT NOT NULL,
	interest MONEY NOT NULL,
)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v13.8.0.0'
WHERE   [name] = 'VERSION'
GO

WITH installments AS
(
    SELECT contract_id, number, capital_repayment
    FROM dbo.Installments i1
    WHERE capital_repayment > paid_capital 
    AND contract_id IN (SELECT id FROM dbo.ActiveLoans(GETDATE(), 0))
),
amounts AS
(
    SELECT contract_id, SUM(capital_repayment) amount
    FROM installments
    GROUP BY contract_id
),
runningTotals AS
(
    SELECT i1.contract_id, i1.number, ISNULL(SUM(i2.capital_repayment), 0) running_total
    FROM installments i1
    LEFT JOIN installments i2 ON i1.contract_id = i2.contract_id AND i1.number > i2.number
    GROUP BY i1.contract_id, i1.number
)

UPDATE i
SET i.olb = upd.olb
FROM dbo.Installments i
INNER JOIN
(
    SELECT rt.contract_id, rt.number, amt.amount - rt.running_total olb
    FROM runningTotals rt
    LEFT JOIN amounts amt ON amt.contract_id = rt.contract_id
) upd ON i.contract_id = upd.contract_id AND i.number = upd.number
