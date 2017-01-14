CREATE TABLE [dbo].[Assets](
	[Id] [uniqueidentifier] NOT NULL,
	[TenantId] [uniqueidentifier] NULL,
	
	[CreateBy] [uniqueidentifier] NULL,
	[CreateTime] [datetime] NULL,
	[ModifyBy] [uniqueidentifier] NULL,
	[ModifyTime] [datetime] NULL)