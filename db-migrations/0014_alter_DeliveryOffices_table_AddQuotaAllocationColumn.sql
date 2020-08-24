IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DeliveryOffices' AND COLUMN_NAME = 'quota_allocation' )
BEGIN
	ALTER TABLE [dbo].[DeliveryOffices]
	ADD [quota_allocation] int not null default 1
END
