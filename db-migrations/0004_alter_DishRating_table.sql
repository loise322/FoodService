IF OBJECT_ID('DishRating') IS NOT NULL
	EXEC sp_rename 'DishRating', 'DishRatings'
GO