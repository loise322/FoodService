IF OBJECT_ID('UserLegals') IS NULL
BEGIN
	CREATE TABLE [dbo].[UserLegals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[legal_id] [int] NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NULL,
     CONSTRAINT [PK_UserLegals] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]

    ALTER TABLE [UserLegals]  WITH CHECK ADD  CONSTRAINT [FK_UserLegals_Legals] FOREIGN KEY([legal_id])
    REFERENCES [Legals] ([id])

    ALTER TABLE [UserLegals] CHECK CONSTRAINT [FK_UserLegals_Legals]

    ALTER TABLE [UserLegals]  WITH CHECK ADD  CONSTRAINT [FK_UserLegals_Users] FOREIGN KEY([user_id])
    REFERENCES [Users] ([id_user])

    ALTER TABLE [dbo].[UserLegals] CHECK CONSTRAINT [FK_UserLegals_Users]

END

IF COL_LENGTH('Users', 'id_team') IS NOT NULL
BEGIN
	INSERT INTO [UserLegals] ( [user_id], [legal_id], [start_date] )
	SELECT [id_user], [id_team], '2018-01-01' FROM [Users] 

    DROP INDEX [id_command] ON [dbo].[Users]

	ALTER TABLE [Users] DROP COLUMN [id_team]
END
