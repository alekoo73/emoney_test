USE [Testapp1]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 27.04.2020 00:44:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [varchar](20) NOT NULL,
	[Balance] [decimal](12, 2) NOT NULL,
	[Owner] [nvarchar](50) NOT NULL,
	[Overdraft] [decimal](12, 2) NOT NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_Acoounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpearationsLogs]    Script Date: 27.04.2020 00:44:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpearationsLogs](
	[Logid] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [varchar](20) NULL,
	[OperationDateTime] [datetime] NOT NULL,
	[IpAddress] [varchar](15) NOT NULL,
	[Request] [nvarchar](max) NULL,
	[Response] [nvarchar](max) NULL,
	[Status] [tinyint] NOT NULL,
 CONSTRAINT [PK_OpearationsLogs] PRIMARY KEY CLUSTERED 
(
	[Logid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 27.04.2020 00:44:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[TransactionId] [varchar](20) NOT NULL,
	[AccountId] [varchar](20) NOT NULL,
	[OperationAmount] [decimal](12, 2) NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Acoounts_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Acoounts_Overdraft]  DEFAULT ((0)) FOR [Overdraft]
GO
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF_Accounts_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[OpearationsLogs]  WITH CHECK ADD  CONSTRAINT [FK_OpearationsLogs_Acoounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[OpearationsLogs] CHECK CONSTRAINT [FK_OpearationsLogs_Acoounts]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_Acoounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_Acoounts]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [CK_Accounts] CHECK  (([Status]=(1) OR [Status]=(0)))
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [CK_Accounts]
GO
