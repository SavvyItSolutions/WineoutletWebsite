use Wineoutlet

CREATE TABLE [dbo].[Products](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [int] UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

Alter table Products
Add Winename [varchar] (max)

Alter table Products
add [Country] [int]

Alter table Products
ADD FOREIGN KEY (Country) REFERENCES Countries(DB_key);

alter table Products
Add [WineType] [int]

alter table Products
ADD FOREIGN KEY (WineType) REFERENCES WineType(DB_key);

insert into Products select '24669','la marca prosecco','1','','','',

select * from Products;

CREATE TABLE [dbo].[Countries](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar] (128) UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

select * from Countries order by DB_key

CREATE TABLE [dbo].[Regions](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[Region] [varchar] (128) UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

insert into SubRegions select 'Prosecco'

select * from Regions

delete from Regions where DB_key=1

CREATE TABLE [dbo].[SubRegions](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[SubRegion] [varchar] (128) UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

select * from SubRegions

CREATE TABLE [dbo].[GrapeType](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[GrapeType] [varchar] (128) UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

insert into GrapeType select 'Prosecco'

select * from GrapeType

CREATE TABLE [dbo].[WineType](
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[WineType] [varchar] (128) UNIQUE,
	
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

insert into WineType select 'Sparkling'

select * from WineType


create table Samplewines(
	[DB_key] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [varchar] (128) not null,
	[WineName] [varchar] (128),
	[Vintage] int ,
	[Country] [varchar] (128),
	[Region] [varchar] (128) ,
	[SubRegion] [varchar] (128) ,
	[GrapeType] [varchar] (128) ,
	[WineType][varchar] (128)
PRIMARY KEY CLUSTERED 
(
	[db_key] ASC
))

select * from Samplewines

insert into Samplewines values ('sku24474','Frescobaldi Tenuta di Castiglioni','2013','Italy','Tuscany','Chianti','MerlotSangiovese','Still wine')

CREATE PROCEDURE [dbo].[InsertSampleData](
	@Sku [varchar] (128) ,
	@WineName [varchar] (128),
	@Vintage int ,
	@Country [varchar] (128),
	@Region [varchar] (128) ,
	@SubRegion [varchar] (128) ,
	@GrapeType [varchar] (128) ,
	@WineType [varchar] (128)
	)
AS  
Begin
insert into Samplewines(Sku,WineName,Vintage,Country,Region,SubRegion,GrapeType,WineType) values(@Sku,@WineName,@Vintage,@Country,@Region,@SubRegion,@GrapeType,@WineType)
end

Exec InsertSampleData 'sku24474','Frescobaldi Tenuta di Castiglioni','2013','Italy','Tuscany',null,'MerlotSangiovese','Still wine'

Alter table Samplewines add Lastupdated DateTime

select * from Samplewines where Vintage='2013'

truncate table Samplewines





