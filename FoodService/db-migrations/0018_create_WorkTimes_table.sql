IF OBJECT_ID('WorkTimes') IS NULL
BEGIN
	CREATE TABLE [dbo].[WorkTimes](
	[id_work_time] [int] IDENTITY(1,1) NOT NULL,
	[month] [date] NOT NULL,
	[id_user] [int] NOT NULL,
	[days] [int] NOT NULL,
	[id_legal] [int] NOT NULL,
	 CONSTRAINT [PK_WorkTimes] PRIMARY KEY CLUSTERED 
	(
		[id_work_time] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	ALTER TABLE [dbo].[WorkTimes]  WITH CHECK ADD  CONSTRAINT [FK_WorkTimes_Legals] FOREIGN KEY([id_legal])
	REFERENCES [dbo].[Legals] ([id])
	
	ALTER TABLE [dbo].[WorkTimes] CHECK CONSTRAINT [FK_WorkTimes_Legals]
	
	ALTER TABLE [dbo].[WorkTimes]  WITH CHECK ADD  CONSTRAINT [FK_WorkTimes_Users] FOREIGN KEY([id_user])
	REFERENCES [dbo].[Users] ([id_user])
	
	ALTER TABLE [dbo].[WorkTimes] CHECK CONSTRAINT [FK_WorkTimes_Users]
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Legals' AND COLUMN_NAME = 'code' )
BEGIN
	ALTER TABLE [dbo].[Legals]
	ADD [code] varchar(10) null
END

GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Legals' AND COLUMN_NAME = 'id_external' )
BEGIN
	ALTER TABLE [dbo].[Legals]
	ADD [id_external] varchar(50) null
END