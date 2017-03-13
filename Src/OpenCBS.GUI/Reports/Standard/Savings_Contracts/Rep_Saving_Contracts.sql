CREATE PROCEDURE [dbo].[Rep_Saving_Contracts]
@pDate DATETIME,
@disbursed_in INT,
@display_in INT
, @branch_id INT
AS 
BEGIN
SELECT      
  COALESCE(pe.last_name+' '+pe.first_name,gr.name,co.name) AS client_name,
  sp.name AS Product_Name, 
  CASE ascm.product_type 	     
	WHEN 'B' THEN 'Saving book' 
	WHEN 'T' THEN 'Term deposit saving' 
	WHEN 'C' THEN 'Compulsory saving'
  END AS Product_type,
  sp.code AS Product_Code, 
  (SELECT MAX(creation_date)
   FROM SavingEvents 
   WHERE contract_id = ascm.contract_id) AS LastEventDate,      
  sc.creation_date AS ContractCreationDate, 
  sc.interest_rate*100 AS interest_rate, 
  ascm.balance AS Balance, 
  sc.code AS Contract_Code
FROM ActiveSavingContracts_MC(@pDate,@disbursed_in,@display_in, @branch_id) ascm
INNER JOIN SavingContracts sc ON sc.id = ascm.contract_id
INNER JOIN SavingProducts sp ON sp.id = sc.product_id
LEFT OUTER JOIN Persons pe ON pe.id = ascm.client_id
LEFT OUTER JOIN Groups gr ON gr.id = ascm.client_id
LEFT OUTER JOIN Corporates co ON co.id = ascm.client_id

END
