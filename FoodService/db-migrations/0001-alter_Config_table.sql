IF OBJECT_ID(N'aaaaaConfig_PK') IS NOT NULL
	EXEC sp_rename 'aaaaaConfig_PK', 'PK_Config'
GO

ALTER TABLE [Config] ALTER COLUMN [param] nvarchar(255) NOT NULL
ALTER TABLE [Config] ALTER COLUMN [paramValue] nvarchar(255) NOT NULL