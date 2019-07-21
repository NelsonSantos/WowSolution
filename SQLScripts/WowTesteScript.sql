SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE usp_Account_Ins 
(
	@holderAccountName varchar(40),
	@accountNumber varchar(10),
	@valueBalance decimal(18, 2),
	@valueLimit decimal(18, 2),
	@blocked bit,
	@guid UNIQUEIDENTIFIER output
)
AS
BEGIN
	set @guid = newid()

	INSERT INTO [dbo].[Account]
           ([Guid]
		   ,[HolderAccountName]
           ,[AccountNumber]
           ,[ValueBalance]
           ,[ValueLimit]
           ,[Blocked])
     VALUES
           (@guid
		   ,@holderAccountName
           ,@accountNumber
		   ,@valueBalance
		   ,@valueLimit
		   ,@blocked)
END
GO

alter PROCEDURE usp_Account_Upd
(
	@guid UNIQUEIDENTIFIER,
	@holderAccountName varchar(40) = null,
	@accountNumber varchar(10) = null,
	@valueBalance decimal(18, 2) = null,
	@valueLimit decimal(18, 2) = null,
	@blocked bit = null
)
AS
BEGIN

	UPDATE [dbo].[Account]
	   SET [HolderAccountName] = isnull(@holderAccountName, HolderAccountName)
		  ,[AccountNumber] = isnull(@accountNumber, AccountNumber)
		  ,[ValueBalance] = isnull(@valueBalance, ValueBalance)
		  ,[ValueLimit] = isnull(@valueLimit, ValueLimit)
		  ,[Blocked] = isnull(@blocked, Blocked)
	 WHERE [Guid] = @Guid

END
GO

alter PROCEDURE usp_Account_Del
(
	@guid UNIQUEIDENTIFIER
)
AS
BEGIN
	DELETE FROM [dbo].[Account] WHERE Guid = @Guid
END
GO

alter PROCEDURE usp_Account_Sel
(
	@guid UNIQUEIDENTIFIER = null
)
AS
BEGIN
	SELECT * FROM [dbo].[Account] WHERE Guid = case when @Guid is null then Guid else @Guid end
END
GO
