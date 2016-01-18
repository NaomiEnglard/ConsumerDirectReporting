USE [WSMProcessing]
GO

/****** Object:  Table [dbo].[RecentMemberPublicRecords]    Script Date: 1/18/2016 3:40:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[RecentMemberPublicRecords](
	[dateRangeFilter] [date] NULL,
	[publisherId] [varchar](15) NULL,
	[Customer_Id] [varchar](15) NULL,
	[Credit_Event_Date] [date] NULL,
	[Member_Date] [date] NULL,
	[Member_City] [varchar](30) NULL,
	[Member_State] [varchar](5) NULL,
	[Member_Phone] [varchar](25) NULL,
	[Member_Code] [varchar](20) NULL,
	[Industry_Code] [varchar](10) NULL,
	[Public_Record_Type] [varchar](15) NULL,
	[Docket_Number] [varchar](30) NULL,
	[Attorney] [varchar](50) NULL,
	[Plaintif] [varchar](50) NULL,
	[Filed_Date] [date] NULL,
	[Paid_Date] [date] NULL,
	[Effective_Date] [date] NULL,
	[Asset] [int] NULL,
	[Liabilities_Amount] [int] NULL,
	[Original_Balance] [int] NULL,
	[Current_Balance] [int] NULL,
	[ECOA_Designator] [varchar](5) NULL,
	[Public_Record_Source_Code] [varchar](10) NULL,
	[Court_Location_City] [varchar](30) NULL,
	[Court_Location_State] [varchar](5) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


