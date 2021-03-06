USE [CityTravelDB]
GO
/****** Object:  Table [dbo].[TransportType]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportType](
	[Id] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TransportType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StopRoutes]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StopRoutes](
	[Stop_Id] [int] NOT NULL,
	[Route_Id] [int] NOT NULL
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Route_Id] ON [dbo].[StopRoutes] 
(
	[Route_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Stop_Id] ON [dbo].[StopRoutes] 
(
	[Stop_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Place]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Place](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[LangId] [int] NULL,
	[PlaceInRussainId] [int] NULL,
	[PlaceBin] [varbinary](max) NULL,
	[Count] [int] NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_LangId] ON [dbo].[Place] 
(
	[LangId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvalidDirection]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvalidDirection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Direction] [nvarchar](max) NULL,
 CONSTRAINT [PK_InvalidDirection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvalidCharacters]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvalidCharacters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ValidWord] [nvarchar](max) NULL,
	[InvalidWord] [nvarchar](max) NULL,
 CONSTRAINT [PK_InvalidCharacters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Building]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Building](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](max) NULL,
	[BuildingIndexNumber] [nvarchar](max) NULL,
	[PlaceId] [int] NULL,
	[BuildingBin] [varbinary](max) NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_PlaceId] ON [dbo].[Building] 
(
	[PlaceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Route](
	[Id] [int] IDENTITY(87,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[RouteBin] [varbinary](max) NULL,
	[RouteType] [int] NULL,
	[RouteGeography] [geography] NULL,
	[WaitingTime] [time](7) NOT NULL,
	[Speed] [int] NOT NULL,
	[Price] [real] NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_RouteType] ON [dbo].[Route] 
(
	[RouteType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stop]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stop](
	[Id] [int] IDENTITY(1387,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[StopBin] [varbinary](max) NULL,
	[StopType] [int] NULL,
	[StopGeography] [geography] NULL,
 CONSTRAINT [PK_Stop] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_StopType] ON [dbo].[Stop] 
(
	[StopType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Trigger [StopGeographyToBinary]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[StopGeographyToBinary]
   ON  [dbo].[Stop]
   AFTER INSERT
AS 
BEGIN
	IF (UPDATE(StopBin))
  begin
    update [Stop] set
   [StopGeography] = geography::STGeomFromWKB(StopBin, 4326)
    where
    Id IN (SELECT Id FROM inserted);
  END
END
GO
/****** Object:  Trigger [RouteGeographyToBinary]    Script Date: 05/04/2012 18:14:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[RouteGeographyToBinary]
   ON  [dbo].[Route]
   AFTER INSERT
AS 
BEGIN
	IF (UPDATE(RouteBin))
  begin
    update [Route] set
   [RouteGeography] = geography::STGeomFromWKB(RouteBin, 4326)
    where
    Id IN (SELECT Id FROM inserted);
  END
END
GO
/****** Object:  Default [DF__Building__Count__1DB06A4F]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Building] ADD  DEFAULT ((0)) FOR [Count]
GO
/****** Object:  Default [DF__Place__Count__1EA48E88]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Place] ADD  DEFAULT ((0)) FOR [Count]
GO
/****** Object:  Default [DF__Route__WaitingTi__09A971A2]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__WaitingTi__09A971A2]  DEFAULT ('00:00:00') FOR [WaitingTime]
GO
/****** Object:  Default [DF__Route__Speed__0A9D95DB]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__Speed__0A9D95DB]  DEFAULT ((0)) FOR [Speed]
GO
/****** Object:  Default [DF__Route__Price__2180FB33]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Route] ADD  DEFAULT ((0)) FOR [Price]
GO
/****** Object:  ForeignKey [FK_Building_Place_PlaceId]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Building]  WITH CHECK ADD  CONSTRAINT [FK_Building_Place_PlaceId] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Place] ([Id])
GO
ALTER TABLE [dbo].[Building] CHECK CONSTRAINT [FK_Building_Place_PlaceId]
GO
/****** Object:  ForeignKey [FK_Place_Place_LangId]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Place]  WITH CHECK ADD  CONSTRAINT [FK_Place_Place_LangId] FOREIGN KEY([LangId])
REFERENCES [dbo].[Place] ([Id])
GO
ALTER TABLE [dbo].[Place] CHECK CONSTRAINT [FK_Place_Place_LangId]
GO
/****** Object:  ForeignKey [FK_Route_TransportType]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_TransportType] FOREIGN KEY([RouteType])
REFERENCES [dbo].[TransportType] ([Id])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_TransportType]
GO
/****** Object:  ForeignKey [FK_Stop_TransportType]    Script Date: 05/04/2012 18:14:08 ******/
ALTER TABLE [dbo].[Stop]  WITH CHECK ADD  CONSTRAINT [FK_Stop_TransportType] FOREIGN KEY([StopType])
REFERENCES [dbo].[TransportType] ([Id])
GO
ALTER TABLE [dbo].[Stop] CHECK CONSTRAINT [FK_Stop_TransportType]
GO
