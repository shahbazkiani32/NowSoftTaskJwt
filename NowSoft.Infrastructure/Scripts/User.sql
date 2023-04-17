﻿USE [nowsoft_db]
GO

/****** Object:  Table [dbo].[User] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[UserName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NULL,
	[Device] [nvarchar](100) NULL,
	[IpAddress] [nvarchar](100) NULL,
    [BalancAmount] [decimal] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO