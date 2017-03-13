CREATE FUNCTION [dbo].[GetSubordinates](@id_user INT)
RETURNS TABLE
AS RETURN
(
 SELECT  *, 
    (SELECT COUNT(*)
	FROM dbo.Credit 
	WHERE loanofficer_id = u.id) AS num_contracts
 FROM  dbo.users u LEFT JOIN dbo.UsersSubordinates us  ON u.id = us.subordinate_id
 WHERE us.user_id  = @id_user AND u.deleted =0 
)
GO


IF not exists (SELECT * FROM sys.tables WHERE name = 'Booking')
BEGIN
CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DebitAccountId] [int] NULL,
	[CreditAccountId] [int] NULL,
	[Amount] [decimal](25, 15) NULL,
	[Date] [datetime] NULL,
	[LoanEventId] [int] NOT NULL DEFAULT(0),
	[SavingEventId] [int] NOT NULL DEFAULT(0),
	[LoanId] [int] NULL,
	[ClientId] [int] NULL,
	[UserId] [int] NULL,
	[BranchId] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[IsExported] [bit] NOT NULL DEFAULT(0),
	[IsDeleted] [bit] NOT NULL DEFAULT(0)
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO

IF not exists (SELECT * FROM sys.tables WHERE name = 'Accounts')
                                                BEGIN 
                                                CREATE TABLE Accounts
                                                (id int identity(1,1),
                                                 account_number varchar(64),
                                                 label nvarchar(256) not null,
                                                 is_debit bit,
                                                 id_category int,
                                                 parent_id int
                                                ) 
END  
GO

IF not exists (SELECT * FROM sys.tables WHERE name = 'Categories')
                                                BEGIN 
                                                CREATE TABLE Categories
												(
												  Id int  identity(1,1),
												  ParentId int,
												  Name nvarchar(128),
												  lft int ,
												  rgt int
												)
END  
GO

IF exists (SELECT * FROM sys.tables WHERE name = 'CustomFieldsValues')
BEGIN
DELETE FROM CustomFieldsValues where owner_id=0
END
GO


IF((Select COUNT(*) from dbo.Categories) = 0 
    or (Select COUNT(*) from dbo.Accounts) = 0)
begin
Insert into dbo.Categories VALUES (0, 'Assets', 0,0)
Insert into dbo.Categories VALUES (0, 'Liabilities', 0,0)
Insert into dbo.Categories VALUES (0, 'Income', 0,0)
Insert into dbo.Categories VALUES (0, 'Expense', 0,0)
Insert into dbo.Categories VALUES (0, 'Equity', 0,0)

Insert into dbo.Categories VALUES (0, 'Expense', 0,0)
Insert into dbo.Categories VALUES (0, 'Equity', 0,0)

--Assets
Insert into dbo.Accounts VALUES (100, 'Current Assets', 1,1,0)
Insert into dbo.Accounts VALUES (101, 'Fixed Assets', 1,1,0)

--100
Insert into dbo.Accounts VALUES (1001, 'Bank', 1,0,1)
Insert into dbo.Accounts VALUES (1002, 'Petty Cash', 1,0,1)
Insert into dbo.Accounts VALUES (1003, 'Checking Account', 1,0,1)
Insert into dbo.Accounts VALUES (1004, 'Short Term Investments', 1,0,1)
Insert into dbo.Accounts VALUES (1005, 'Loan Receivables', 1,0,1)
Insert into dbo.Accounts VALUES (1006, 'Accrued Interest Receivable', 1,0,1)
Insert into dbo.Accounts VALUES (1007, 'Accumulated Loan Loss Provision', 0,0,1)
Insert into dbo.Accounts VALUES (1008, 'Accounts Receivable', 1,0,1)
Insert into dbo.Accounts VALUES (1009, 'Other Assets', 1,0,1)

--1001
Insert into dbo.Accounts VALUES (10010, 'Cash', 1,0,3)
Insert into dbo.Accounts VALUES (10012, 'Cheque', 1,0,3)
Insert into dbo.Accounts VALUES (10013, 'WT', 1,0,3)
Insert into dbo.Accounts VALUES (10014, 'Transit', 1,0,3)

--1005
Insert into dbo.Accounts VALUES (10051, 'Current Loan Receivables', 1,0,7)
Insert into dbo.Accounts VALUES (10052, 'Overdue Loan Receivables', 1,0,7)

--1006
Insert into dbo.Accounts VALUES (10061, 'Accrued Current Interest Receivable', 1,0,8)
Insert into dbo.Accounts VALUES (10062, 'Accrued Late Interest Receivable', 1,0,8)

--Liabilities
Insert into dbo.Accounts VALUES (200, 'Current Liabilities', 0,2,0)

--200
Insert into dbo.Accounts VALUES (2001, 'Accounts Payable', 0,0,20)
Insert into dbo.Accounts VALUES (2002, 'Accrued Expenses', 0,0,20)
Insert into dbo.Accounts VALUES (2003, 'Payroll Liabities', 0,0,20)
Insert into dbo.Accounts VALUES (2004, 'Accrued Interest Expense', 0,0,20)
Insert into dbo.Accounts VALUES (2005, 'Accrued Payroll Expense', 0,0,20)
Insert into dbo.Accounts VALUES (2006, 'Accrued Payroll Taxes', 0,0,20)
Insert into dbo.Accounts VALUES (2007, 'Federal Income Tax Payable', 0,0,20)
Insert into dbo.Accounts VALUES (2008, 'State/Local Income Tax Payable', 0,0,20)
Insert into dbo.Accounts VALUES (2009, 'Line of Credit', 0,0,20)
Insert into dbo.Accounts VALUES (2010, 'Term Loan', 0,0,20)
Insert into dbo.Accounts VALUES (2011, 'Note Payable', 0,0,20)
Insert into dbo.Accounts VALUES (2012, 'Debentures', 0,0,20)

--Income 
Insert into dbo.Accounts VALUES (300, 'Income', 0,3,0)

--300
Insert into dbo.Accounts VALUES (3001, 'Interest Income', 0,0,33)
Insert into dbo.Accounts VALUES (3002, 'Processing Fees', 0,0,33)
Insert into dbo.Accounts VALUES (3003, 'Late Fees', 0,0,33)
Insert into dbo.Accounts VALUES (3004, 'Misc. Fees', 0,0,33)
Insert into dbo.Accounts VALUES (3005, 'Recovery Income', 0,0,33)
Insert into dbo.Accounts VALUES (3006, 'Finance Income', 0,0,33)
Insert into dbo.Accounts VALUES (3007, 'Other Income', 0,0,33)
Insert into dbo.Accounts VALUES (3008, 'Accrued Interest', 0,0,33)

--3001
Insert into dbo.Accounts VALUES (30011, 'Current Interest', 0,0,34)
Insert into dbo.Accounts VALUES (30012, 'Late Interest', 0,0,34)

--Expense
Insert into dbo.Accounts VALUES (400, 'Expense', 1,4,0)

--400
Insert into dbo.Accounts VALUES (4001, 'Interest Expense', 1,0,44)
Insert into dbo.Accounts VALUES (4002, 'Loan Loss Provisioning', 1,0,44)
Insert into dbo.Accounts VALUES (4003, 'Penalties', 1,0,44)
Insert into dbo.Accounts VALUES (4004, 'Professional Fees', 1,0,44)

--Equity
Insert into dbo.Accounts VALUES (500, 'Equity', 0,5,0)

--500
Insert into dbo.Accounts VALUES (5001, 'Common Shares', 0,0,49)
Insert into dbo.Accounts VALUES (5002, 'Paid in Capital',0,0,49)
end
GO

IF not exists (Select * from dbo.GeneralParameters WHERE [key] = 'USE_EXTERNAL_ACCOUNNING')
BEGIN
INSERT INTO [GeneralParameters]([key], [value]) VALUES('USE_EXTERNAL_ACCOUNTING', 1)
END
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v14.10.0.0'
WHERE   [name] = 'VERSION'
GO