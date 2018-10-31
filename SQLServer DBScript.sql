USE [master]
GO

/****** Object:  Database [CompanyDB]    Script Date: 10/30/2018 10:51:07 PM ******/
CREATE DATABASE [CompanyDB]
GO

USE [CompanyDB]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 10/30/2018 10:54:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](75) NOT NULL,
	[LastName] [varchar](75) NULL,
	[Email] [varchar](50) NOT NULL,
	[PhoneNumber] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

--Insert Values to the table
Insert Company
values('WebWorks','International','info@webworks.com',9563897515)
Insert Company
values('Premier Technologies','Pvt Ltd','info@premiertechnologies.com', 7845236432)
Insert Company
values('Adobe','International','info@adobe.com', 7844152454)
Insert Company
values('DYC Australia','Pvt Ltd','info@dycaustralia.org.au', 4512987367)
Insert Company
values('Hindustan Level','Pvt Ltd', 'info@hindustanleverlimited.com', 1456925762)
