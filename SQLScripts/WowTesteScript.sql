SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[Guid] [uniqueidentifier] NOT NULL,
	[HolderAccountName] [varchar](40) NOT NULL,
	[AccountNumber] [varchar](10) NOT NULL,
	[ValueBalance] [decimal](18, 2) NOT NULL,
	[ValueLimit] [decimal](18, 2) NOT NULL,
	[Blocked] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

create PROCEDURE usp_Account_Ins 
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

create PROCEDURE usp_Account_Upd
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

create PROCEDURE usp_Account_Del
(
	@guid UNIQUEIDENTIFIER
)
AS
BEGIN
	DELETE FROM [dbo].[Account] WHERE Guid = @Guid
END
GO

create PROCEDURE usp_Account_Sel
(
	@guid UNIQUEIDENTIFIER = null
)
AS
BEGIN
	SELECT * FROM [dbo].[Account] WHERE Guid = case when @Guid is null then Guid else @Guid end
END
GO
