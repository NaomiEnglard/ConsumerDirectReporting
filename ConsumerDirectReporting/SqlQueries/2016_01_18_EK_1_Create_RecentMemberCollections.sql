USE [WSMProcessing]
GO

/****** Object:  Table [dbo].[RecentMemberCollections]    Script Date: 1/18/2016 3:37:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RecentMemberCollections](
	[dateRangeFilter] [date] NULL,
	[publisherId] [varchar](15) NULL,
	[Customer_Id] [varchar](15) NULL,
	[Credit_Event_Date] [date] NULL,
	[Member_Date] [date] NULL,
	[Member_City] [varchar](30) NULL,
	[Member_State] [varchar](5) NULL,
	[Member_Phone] [varchar](25) NULL,
	[Member_Code_Account_Id] [varchar](20) NULL,
	[Industry_Code] [varchar](10) NULL,
	[Member_Code] [varchar](15) NULL,
	[Collection_Agency] [varchar](50) NULL,
	[Portfolio_Type] [varchar](5) NULL,
	[Account_Number] [varchar](50) NULL,
	[Account_Type] [varchar](10) NULL,
	[ECOA_Designator] [varchar](5) NULL,
	[Current_Account_Rating] [varchar](5) NULL,
	[Affiliate_Remark_Code] [varchar](5) NULL,
	[Generic_Remark_Code] [varchar](15) NULL,
	[Rating_History_Remark_Code] [varchar](10) NULL,
	[Compliance_Remark_Code] [varchar](10) NULL,
	[Update_Method_Code] [varchar](5) NULL,
	[Original_Creditor_Name] [varchar](50) NULL,
	[Creditor_Classification] [int] NULL,
	[Current_Balance_Amount] [int] NULL,
	[Original_Balance_Amount] [int] NULL,
	[Past_Due_Amount] [int] NULL,
	[Actual_Payment_Amount] [int] NULL,
	[Opened_Date] [date] NULL,
	[Closed_Date_Indicator] [varchar](5) NULL,
	[Effective_Date] [date] NULL,
	[Paid_Out_Date] [date] NULL,
	[Effective] [varchar](30) NULL,
	[First_Delinquency_Date] [date] NULL,
	[Last_Payment_Date] [date] NULL,
	[Closed_Date] [date] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


