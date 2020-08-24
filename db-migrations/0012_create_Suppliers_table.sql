IF OBJECT_ID('Suppliers') IS NULL
BEGIN
	  CREATE TABLE [dbo].[Suppliers](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[name] [nvarchar](255) NOT NULL,
		[address] [nvarchar](255) NOT NULL,
		[contact_person] [nvarchar](255) NULL,
		[email] [nvarchar](100) NULL,
		[phone] [nvarchar](50) NULL,
		[legal_entity] [nvarchar](255) NOT NULL,
		[discount] [int] NOT NULL,
		[salat_ware_cost] [int] NOT NULL,
		[soup_ware_cost] [int] NOT NULL,
		[second_ware_cost] [int] NOT NULL,
	 CONSTRAINT [PK_Supplier] PRIMARY KEY NONCLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

END

