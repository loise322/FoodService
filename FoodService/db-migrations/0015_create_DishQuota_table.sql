﻿IF OBJECT_ID('DishQuota') IS NULL
BEGIN
	  CREATE TABLE [dbo].[DishQuotas](
		[id_dish] [int] NOT NULL,
		[date] [datetime] NOT NULL,
		[quota] [int] not NULL,
	 CONSTRAINT [PK_DishQuotas] PRIMARY KEY NONCLUSTERED 
	(
		[id_dish] ASC,
		[date] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
