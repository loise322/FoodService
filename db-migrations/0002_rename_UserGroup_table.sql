IF OBJECT_ID('UserGroup') IS NOT NULL
	EXEC sp_rename 'UserGroup', 'DeliveryOffices'
GO

IF COL_LENGTH('DeliveryOffices', 'id_group') IS NOT NULL
   EXEC sp_rename 'DeliveryOffices.id_group', 'id_delivery_office', 'COLUMN'; 
GO

IF COL_LENGTH('DeliveryOffices', 'group_name') IS NOT NULL
   EXEC sp_rename 'DeliveryOffices.group_name', 'name', 'COLUMN'; 
GO

IF OBJECT_ID('aaaaaUserGroup_PK') IS NOT NULL
	EXEC sp_rename 'aaaaaUserGroup_PK', 'PK_DeliveryOffices'
GO

ALTER TABLE [DeliveryOffices] ALTER COLUMN [name] nvarchar(50) NOT NULL;