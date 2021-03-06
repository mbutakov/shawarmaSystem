USE [fastfood]
GO
/****** Object:  User [fastFood]    Script Date: 23.04.2022 8:56:36 ******/
CREATE USER [fastFood] FOR LOGIN [fastFood] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [fastFood]
GO
ALTER ROLE [db_datareader] ADD MEMBER [fastFood]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [fastFood]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Phone] [varchar](10) NOT NULL,
	[FullName] [varchar](200) NOT NULL,
	[Address] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dish]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dish](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishCompound]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishCompound](
	[Dish] [int] NOT NULL,
	[Ingredient] [int] NOT NULL,
	[Volume] [decimal](10, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Dish] ASC,
	[Ingredient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishImage]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishImage](
	[Dish] [int] NOT NULL,
	[Image] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Dish] ASC,
	[Image] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishStatus]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishStatus](
	[Name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Phone] [varchar](10) NOT NULL,
	[Password] [varchar](18) NOT NULL,
	[Name] [varchar](32) NOT NULL,
	[Surname] [varchar](32) NOT NULL,
	[Patronymic] [varchar](32) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [varchar](100) NOT NULL,
	[URL] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[Articule] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Unit] [varchar](20) NOT NULL,
	[Image] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Articule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Client] [varchar](10) NOT NULL,
	[Employee] [varchar](10) NOT NULL,
	[Date] [datetime] NULL,
	[Status] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order2]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order2](
	[id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderCompound]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderCompound](
	[Order] [int] NOT NULL,
	[Dish] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[Count] [int] NOT NULL,
	[Status] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Order] ASC,
	[Dish] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Unit]    Script Date: 23.04.2022 8:56:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unit](
	[Name] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[DishCompound]  WITH CHECK ADD  CONSTRAINT [DishCompound_fk0] FOREIGN KEY([Dish])
REFERENCES [dbo].[Dish] ([ID])
GO
ALTER TABLE [dbo].[DishCompound] CHECK CONSTRAINT [DishCompound_fk0]
GO
ALTER TABLE [dbo].[DishCompound]  WITH CHECK ADD  CONSTRAINT [DishCompound_fk1] FOREIGN KEY([Ingredient])
REFERENCES [dbo].[Ingredient] ([Articule])
GO
ALTER TABLE [dbo].[DishCompound] CHECK CONSTRAINT [DishCompound_fk1]
GO
ALTER TABLE [dbo].[DishImage]  WITH CHECK ADD  CONSTRAINT [DishImage_fk0] FOREIGN KEY([Dish])
REFERENCES [dbo].[Dish] ([ID])
GO
ALTER TABLE [dbo].[DishImage] CHECK CONSTRAINT [DishImage_fk0]
GO
ALTER TABLE [dbo].[DishImage]  WITH CHECK ADD  CONSTRAINT [DishImage_fk1] FOREIGN KEY([Image])
REFERENCES [dbo].[Image] ([ID])
GO
ALTER TABLE [dbo].[DishImage] CHECK CONSTRAINT [DishImage_fk1]
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD  CONSTRAINT [Ingredient_fk0] FOREIGN KEY([Unit])
REFERENCES [dbo].[Unit] ([Name])
GO
ALTER TABLE [dbo].[Ingredient] CHECK CONSTRAINT [Ingredient_fk0]
GO
ALTER TABLE [dbo].[OrderCompound]  WITH CHECK ADD  CONSTRAINT [OrderCompound_fk1] FOREIGN KEY([Dish])
REFERENCES [dbo].[Dish] ([ID])
GO
ALTER TABLE [dbo].[OrderCompound] CHECK CONSTRAINT [OrderCompound_fk1]
GO
ALTER TABLE [dbo].[OrderCompound]  WITH CHECK ADD  CONSTRAINT [OrderCompound_fk2] FOREIGN KEY([Status])
REFERENCES [dbo].[DishStatus] ([Name])
GO
ALTER TABLE [dbo].[OrderCompound] CHECK CONSTRAINT [OrderCompound_fk2]
GO



USE [fastfood]
GO
INSERT INTO [dbo].[DishStatus]([Name])VALUES('В ожиданий')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[DishStatus]([Name])VALUES('В работе')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[DishStatus]([Name])VALUES('Готов к выдаче')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[DishStatus]([Name])VALUES('Приготовлен')
GO


USE [fastfood]
GO
INSERT INTO [dbo].[OrderStatus]([Name])VALUES('В ожиданий')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[OrderStatus]([Name])VALUES('В работе')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[OrderStatus]([Name])VALUES('Готов к выдаче')
GO
USE [fastfood]
GO
INSERT INTO [dbo].[OrderStatus]([Name])VALUES('Приготовлен')
GO

USE [fastfood]
GO

INSERT INTO [dbo].[Unit]
           ([Name])
     VALUES
           ('грамм')
GO



USE [fastfood]
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Томат','грамм','tomato')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Курица','грамм','chicken')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Капуста','грамм','capusta')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Огурец','грамм','cucumber')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Белый соус','грамм','white_sauce')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Красный соус','грамм','red_sauce')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Халапеньо','грамм','xalapeshka')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Лаваш','грамм','lavash')
GO
INSERT INTO [dbo].[Ingredient]
           ([Name]
           ,[Unit]
           ,[Image])
     VALUES
           ('Картошка','грамм','potato')
GO


INSERT INTO [dbo].[Employee]
           ([Phone]
           ,[Password]
           ,[Name]
           ,[Surname]
           ,[Patronymic])
     VALUES
           ('9025927723'
           ,'123'
           ,'Матвей'
           ,'Бутаков'
           ,'123')
GO



INSERT INTO [dbo].[Employee]
           ([Phone]
           ,[Password]
           ,[Name]
           ,[Surname]
           ,[Patronymic])
     VALUES
           ('9025927720'
           ,'123'
           ,'Иван'
           ,'Петрович'
           ,'1234')
GO




