create database wineoutlet_dev;

use wineoutlet_dev
 
CREATE TABLE [dbo].[WOCustomers](
	[Db_key] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] UNIQUE,
	[FirstName] [varchar](256) NOT NULL,
	[LastName] [varchar](256) NOT NULL,
	[email] [varchar](max) NULL,
	[mobile] [varchar](64) NOT NULL,
	[Gender] [char](64) NOT NULL,
	[Address] [varchar](max) NULL,
	[dob] [datetime] NULL,
	[country] [varchar](64) NULL,
	[city] [varchar](64) NULL,
	[password] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Db_key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

select * from [WOCustomers]

drop table [WOCustomers]

CREATE TABLE [dbo].[Orders](
	[db_key] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] UNIQUE,
	[DateofOrder] [datetime] NULL,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

select * from [Orders]
