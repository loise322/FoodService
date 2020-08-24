IF OBJECT_ID('UserTeam') IS NOT NULL
	EXEC sp_rename 'UserTeam', 'Legals'
GO

IF COL_LENGTH('Legals', 'id_team') IS NOT NULL
	EXEC sp_rename 'Legals.id_team', 'id', 'COLUMN'
GO

IF COL_LENGTH('Legals', 'team_name') IS NOT NULL
	EXEC sp_rename 'Legals.team_name', 'name', 'COLUMN'
GO

IF COL_LENGTH('Legals', 'full_name') IS NOT NULL
	EXEC sp_rename 'Legals.full_name', 'full_name', 'COLUMN'
GO

IF COL_LENGTH('Legals', 'is_deleted') IS NULL
	ALTER TABLE [Legals] ADD [is_deleted] bit NULL
	GO
	
	UPDATE [Legals] SET [is_deleted] = 0 WHERE [is_deleted] IS NULL
    GO

    ALTER TABLE [Legals] ALTER COLUMN [is_deleted] bit NOT NULL
	GO
GO

IF OBJECT_ID('aaaaaUserTeam_PK') IS NOT NULL
	EXEC sp_rename 'aaaaaUserTeam_PK', 'PK_Legal';
GO

ALTER TABLE [Legals] ALTER COLUMN [name] nvarchar(255) NOT NULL;
