IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Dish' AND COLUMN_NAME = 'id_supplier' )
BEGIN
	ALTER TABLE [dbo].[Dish]
	ADD [id_supplier] int not null default 0
END
