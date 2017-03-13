INSERT INTO dbo.GeneralParameters ([key], value)
values ('SHORT_DATE_FORMAT', 'dd-MM-yyyy')
, ('STANDARD_ZIP_CODE', '')
,('STANDARD_MOBILE_PHONE_FORMAT', '')
,('STANDARD_CITY_PHONE_FORMAT', '');

UPDATE  [TechnicalParameters]
SET     [value] = 'v15.1.0.0'
WHERE   [name] = 'VERSION'
GO