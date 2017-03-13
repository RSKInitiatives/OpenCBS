alter table Accounts alter column account_number varchar(32) not null
GO

alter table Accounts add constraint account_number_primary_key primary key(account_number)
GO

alter table Accounts add is_balance_account bit default(0), parent varchar(32)
GO

update a
set parent = a1.account_number
from Accounts a
left join Accounts a1 on a1.id=a.parent_id
GO

alter table Accounts add foreign key (parent) references Accounts(account_number)
GO

alter table Accounts add lft int not null default(0),
rgt int not null default(0)
GO

-- Tree holds the adjacency model 
DECLARE @Tree TABLE 
(
	child int NOT NULL
	, parent int
)
-- A fake root account
INSERT INTO @Tree
SELECT '0', NULL
-- Accounts
INSERT INTO @Tree
SELECT id, ISNULL(parent_id, 0)
FROM Accounts

-- Stack starts empty, will hold the nested set model 
DECLARE @Stack TABLE
(
	stack_top INT NOT NULL
	, child INT NOT NULL
	, lft INT
	, rgt INT
)

DECLARE @lft_rgt INT
	, @stack_pointer INT
	, @max_lft_rgt INT
	
SET @max_lft_rgt = 2 * (SELECT COUNT(*) FROM @Tree)
INSERT INTO @Stack SELECT 1, child, 1, @max_lft_rgt 
FROM @Tree 
WHERE parent IS NULL

SET @lft_rgt = 2
SET @Stack_pointer = 1
DELETE FROM @Tree WHERE parent IS NULL

-- The Stack is now loaded and ready to use
WHILE (@lft_rgt < @max_lft_rgt) 
BEGIN 
	IF EXISTS (SELECT * FROM @Stack AS S1, @Tree AS T1 WHERE S1.child = T1.parent AND S1.stack_top = @stack_pointer) 
	BEGIN -- push when stack_top has subordinates and set lft value 
		INSERT INTO @Stack 
		SELECT (@stack_pointer + 1), MIN(T1.child), @lft_rgt, NULL 
		FROM @Stack AS S1, @Tree AS T1 WHERE S1.child = T1.parent AND S1.stack_top = @stack_pointer;
		
		-- remove this row from Tree 
		DELETE FROM @Tree WHERE child = (SELECT child FROM @Stack WHERE stack_top = @stack_pointer + 1)
		SET @stack_pointer = @stack_pointer + 1
	END -- push 
	ELSE 
	BEGIN -- pop the Stack and set rgt value 
		UPDATE @Stack SET rgt = @lft_rgt, stack_top = -stack_top 
		WHERE stack_top = @stack_pointer
		
		SET @stack_pointer = @stack_pointer - 1
	END -- pop 
	SET @lft_rgt = @lft_rgt + 1
END -- WHILE

DELETE FROM @Stack WHERE child = 0
UPDATE @Stack SET lft = lft - 1, rgt = rgt - 1

UPDATE coa
SET coa.lft = s.lft, coa.rgt = s.rgt
FROM Accounts coa
LEFT JOIN @Stack s ON s.child = coa.id
GO

alter table Booking add DebitAccount varchar(32), CreditAccount varchar(32)
GO

update b
set b.DebitAccount = debit.account_number,
b.CreditAccount=credit.account_number
from Booking b
left join Accounts debit on debit.id=b.DebitAccountId
left join Accounts credit on credit.id=b.CreditAccountId
GO

alter table Booking add foreign key (DebitAccount) references Accounts(account_number)
GO

alter table Booking add foreign key (CreditAccount) references Accounts(account_number)
GO

alter table Booking drop column DebitAccountId, CreditAccountId
GO

alter table Accounts drop column id, parent_id
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.14.0.0'
WHERE   [name] = 'VERSION'
GO