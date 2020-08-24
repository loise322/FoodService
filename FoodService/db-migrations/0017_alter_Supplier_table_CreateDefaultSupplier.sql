IF NOT EXISTS (SELECT * FROM [dbo].[Suppliers])
BEGIN
	INSERT INTO [dbo].[Suppliers]
           ([name]
           ,[address]
           ,[contact_person]
           ,[email]
           ,[phone]
           ,[legal_entity]
           ,[discount]
           ,[salat_ware_cost]
           ,[soup_ware_cost]
           ,[second_ware_cost])
     VALUES
           (N'Визит','','','','',N'ООО "Интерлайн"',10,3,9,5);
	
	UPDATE Dish SET id_supplier = @@IDENTITY;

END