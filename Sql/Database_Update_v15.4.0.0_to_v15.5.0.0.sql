if exists(select * from sys.columns 
            where Name = N'type_id' and Object_ID = Object_ID(N'city'))
begin
    alter table city 
	add default '1' for type_id
end
GO

INSERT INTO MenuItems ([component_name], [type])
VALUES ('_viewItem', 0)
, ('_startPageItem', 0)
, ('_alertsItem' , 0)
, ('_dashboardItem', 0)
GO

INSERT INTO menuitems([component_name], [type])
VALUES ('_modulesMenuItem', 0)
, ('_aboutModulesMenuItem', 0)
, ('mnuCustomFields', 0)
, ('ClientLocation', 0)
, ('mnuEventFields', 0)
, ('DMM', 0)
, ('FastRepaymentMenuItem', 0)
, ('TaskManagementMenuItem', 0)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.5.0.0'
WHERE   [name] = 'VERSION'
GO