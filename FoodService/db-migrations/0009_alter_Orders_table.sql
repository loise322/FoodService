IF COL_LENGTH('Orders', 'id_order') IS NULL
BEGIN
	ALTER TABLE [Orders] Add [id_order] int Identity(1,1) 
	ALTER TABLE [Orders] add primary key ([id_order])
END
GO

IF OBJECT_ID('DF__Orders__garnish__1273C1CD') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__garnish__1273C1CD] 
GO

IF OBJECT_ID('DF__Orders__salat__108B795B') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__salat__108B795B] 
GO

IF OBJECT_ID('DF__Orders__second_d__1367E606') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__second_d__1367E606] 
GO

IF OBJECT_ID('DF__Orders__soup__117F9D94') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__soup__117F9D94] 
GO

IF OBJECT_ID('DF__Orders__id_user__0F975522') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__id_user__0F975522] 
GO

IF OBJECT_ID('DF__Orders__cost__145C0A3F') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [DF__Orders__cost__145C0A3F] 
GO

IF OBJECT_ID('Orders_FK00') IS NOT NULL 
	ALTER TABLE [Orders]
	DROP CONSTRAINT [Orders_FK00] 
GO

IF COL_LENGTH('Orders', 'cost') IS NOT NULL
BEGIN
	IF OBJECT_ID('DF__Orders__cost__145C0A3F') IS NOT NULL 
		ALTER TABLE [Orders] DROP CONSTRAINT [DF__Orders__cost__145C0A3F] 
		
	ALTER TABLE [Orders] ALTER COLUMN [cost] decimal(10,2) NOT NULL
END
GO

DELETE FROM [dbo].[Orders] WHERE [order_date] < '2018-01-01'
GO

IF COL_LENGTH('Orders', 'dishes_cost') IS NOT NULL
BEGIN
    UPDATE [Orders] SET [dishes_cost] = 0 where [dishes_cost] IS NULL

	ALTER TABLE [Orders] ALTER COLUMN [dishes_cost] decimal(10,2) NOT NULL
END
GO

BEGIN
	IF COL_LENGTH('Orders', 'id_team') IS NOT NULL
		EXEC sp_rename 'Orders.id_team', 'id_legal', 'COLUMN'
END
GO

IF COL_LENGTH('Orders', 'breadCount') IS NOT NULL
BEGIN
	ALTER TABLE [Orders] DROP COLUMN breadCount;
END
GO



UPDATE [Orders] SET [salat] = NULL WHERE [salat] = 0
UPDATE [Orders] SET [garnish] = NULL WHERE [garnish] = 0
UPDATE [Orders] SET [soup] = NULL WHERE [soup] = 0
UPDATE [Orders] SET [second_dish] = NULL WHERE [second_dish] = 0
GO

IF OBJECT_ID('FK_Orders_Dish_garnish') IS NULL
BEGIN
	ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Dish_garnish] FOREIGN KEY([garnish])
	REFERENCES [dbo].[Dish] ([id_dish])
END

IF OBJECT_ID('FK_Orders_Dish_salat') IS NULL
BEGIN
	ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Dish_salat] FOREIGN KEY([salat])
	REFERENCES [dbo].[Dish] ([id_dish])
END

IF OBJECT_ID('FK_Orders_Dish_second') IS NULL
BEGIN
	ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Dish_second] FOREIGN KEY([second_dish])
	REFERENCES [dbo].[Dish] ([id_dish])
END

IF OBJECT_ID('FK_Orders_Dish_soup') IS NULL
BEGIN
	ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Dish_soup] FOREIGN KEY([soup])
	REFERENCES [dbo].[Dish] ([id_dish])
END

IF OBJECT_ID('FK_Orders_Users') IS NULL
BEGIN
	ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([id_user])
	REFERENCES [dbo].[Users] ([id_user])
END

IF IndexProperty(Object_Id('Orders'), 'IX_Orders_order_date_id_user', 'IndexId') IS NULL
BEGIN
	CREATE NONCLUSTERED INDEX [IX_Orders_order_date_id_user] ON [dbo].[Orders]
	(
		[order_date] ASC,
		[id_user] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
