IF COL_LENGTH('Dish', 'dish_name') IS NOT NULL
	ALTER TABLE Dish ALTER COLUMN [dish_name] nvarchar(255) NOT NULL
GO

IF COL_LENGTH('Dish', 'dish_name') IS NOT NULL
	EXEC sp_rename 'Dish.dish_name', 'name'
GO

IF OBJECT_ID('DF__Dish__type__1B0907CE') IS NOT NULL 
	ALTER TABLE Dish
	DROP CONSTRAINT [DF__Dish__type__1B0907CE] 
GO

IF OBJECT_ID('DF__Dish__cost__1BFD2C07') IS NOT NULL 
	ALTER TABLE Dish
	DROP CONSTRAINT [DF__Dish__cost__1BFD2C07] 
GO

IF OBJECT_ID('DF__Dish__weight__1CF15040') IS NOT NULL 
	ALTER TABLE Dish
	DROP CONSTRAINT [DF__Dish__weight__1CF15040] 
GO

IF COL_LENGTH('Dish', 'cost') IS NOT NULL
	ALTER TABLE Dish ALTER COLUMN [cost] decimal(10,2) NOT NULL
GO

IF COL_LENGTH('Dish', 'type') IS NOT NULL
	ALTER TABLE Dish ALTER COLUMN [type] tinyint NOT NULL
GO

IF COL_LENGTH('Dish', 'single') IS NOT NULL
	UPDATE Dish SET [single]=0 WHERE [single] IS NULL
	ALTER TABLE Dish ALTER COLUMN [single] bit
GO

IF COL_LENGTH('Dish', 'weight') IS NOT NULL
	UPDATE Dish SET [weight]=0 WHERE [weight] IS NULL
	ALTER TABLE Dish ALTER COLUMN [weight] int NOT NULL
GO

IF OBJECT_ID('aaaaaDish_PK') IS NOT NULL
	EXEC sp_rename 'aaaaaDish_PK', 'PK_Dish'
GO
