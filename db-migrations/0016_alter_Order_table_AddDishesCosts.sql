IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Orders' AND COLUMN_NAME = 'salat_cost' )
BEGIN
	ALTER TABLE [dbo].[Orders]
	ADD [salat_cost] decimal(10,2) null
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Orders' AND COLUMN_NAME = 'soup_cost' )
BEGIN
	ALTER TABLE [dbo].[Orders]
	ADD [soup_cost] decimal(10,2) null
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Orders' AND COLUMN_NAME = 'garhish_cost' )
BEGIN
	ALTER TABLE [dbo].[Orders]
	ADD [garhish_cost] decimal(10,2) null
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Orders' AND COLUMN_NAME = 'second_dish_cost' )
BEGIN
	ALTER TABLE [dbo].[Orders]
	ADD [second_dish_cost] decimal(10,2) null
END