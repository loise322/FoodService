IF COL_LENGTH('Users', 'id_group') IS NOT NULL
BEGIN
	EXEC sp_rename 'Users.id_group', 'id_delivery_office', 'COLUMN'
END

IF COL_LENGTH('Users', 'user_name') IS NOT NULL
BEGIN
	EXEC sp_rename 'Users.user_name', 'name', 'COLUMN'
END

IF COL_LENGTH('Users', 'code') IS NULL
	ALTER TABLE [Users] ADD [code] varchar(10) NULL
GO

IF COL_LENGTH('Users', 'id_external') IS NULL
	ALTER TABLE [Users] ADD [id_external] varchar(50) NULL
GO

IF OBJECT_ID(N'aaaaaUsers_PK') IS NOT NULL
	EXEC sp_rename 'aaaaaUsers_PK', 'PK_Users'
GO

IF OBJECT_ID(N'Users_FK00') IS NOT NULL
	EXEC sp_rename 'Users_FK00', 'FK_Users_DeliveryOffices'
GO
