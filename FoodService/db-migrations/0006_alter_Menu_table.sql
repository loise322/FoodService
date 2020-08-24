IF OBJECT_ID('Menu') IS NOT NULL
	EXEC sp_rename 'Menu', 'Menus'
GO

IF COL_LENGTH('Menus', 'is_ordered') IS NOT NULL
BEGIN
	DROP INDEX IF EXISTS [is_ordered] ON [Menus]
	ALTER TABLE [Menus]
	ALTER COLUMN [is_ordered] bit NOT NULL
END
GO

IF COL_LENGTH('Menus', 'id') IS NULL
BEGIN
	ALTER TABLE Menus Add id int Identity(1,1) 
	ALTER TABLE Menus add primary key ([id])
END
GO

ALTER TABLE [Menus] ALTER COLUMN [menu] ntext NOT NULL;