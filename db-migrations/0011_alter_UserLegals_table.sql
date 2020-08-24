IF COL_LENGTH('UserLegals', 'end_date') IS NOT NULL
	ALTER TABLE UserLegals DROP COLUMN end_date

GO

IF COL_LENGTH('UserLegals', 'transfer_reason') IS NULL
	ALTER TABLE UserLegals 
	ADD [transfer_reason] tinyint NOT NULL
	CONSTRAINT D_transfer_reason DEFAULT 0