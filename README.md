#MusicLog 

# Welcome to the MusicLog, your Music Store API

<details><summary>Requirements</summary>
1- Microsoft .Net 6.0 (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2- Microsoft Sql Server express or other versions (https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)    
</details>

<details><summary>Installation</summary>
## 1- Create the Databsase 
Run the Script SQL below to create the database. The same script can be found in the directory *scripts\001 - CreateDatabase.sql* 
```sql
use [master]
go
create database MusicLogDB
GO
USE [MusicLogDB]
GO
/****** Object:  Table [dbo].[AlbumArtist]    Script Date: 16/01/2022 21:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumArtist](
[AlbumsId] [int] NOT NULL,
[ArtistsId] [int] NOT NULL,
CONSTRAINT [PK_AlbumArtist] PRIMARY KEY CLUSTERED 
(
[AlbumsId] ASC,
[ArtistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Albums]    Script Date: 16/01/2022 21:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Albums](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Title] [nvarchar](max) NOT NULL,
[AlbumType] [int] NOT NULL,
[Stock] [bit] NOT NULL,
CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artists]    Script Date: 16/01/2022 21:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artists](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [nvarchar](max) NOT NULL,
CONSTRAINT [PK_Artists] PRIMARY KEY CLUSTERED 
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AlbumArtist]  WITH CHECK ADD  CONSTRAINT [FK_AlbumArtist_Albums_AlbumsId] FOREIGN KEY([AlbumsId])
REFERENCES [dbo].[Albums] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlbumArtist] CHECK CONSTRAINT [FK_AlbumArtist_Albums_AlbumsId]
GO
ALTER TABLE [dbo].[AlbumArtist]  WITH CHECK ADD  CONSTRAINT [FK_AlbumArtist_Artists_ArtistsId] FOREIGN KEY([ArtistsId])
REFERENCES [dbo].[Artists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlbumArtist] CHECK CONSTRAINT [FK_AlbumArtist_Artists_ArtistsId]
GO
````
---
## 2- Configure the Application*
Into the folder *src\Musicalog.Api\appsettings.json*  alter the *ConnectionString* to configure for you environment  
For example:
```
"ConnectionStrings": { "MusicLogDb": "Data Source=.;Initial Catalog=MusicLogDB;Persist Security Info=True;User ID=SA;Password=1q2w3e4r!Q@W#E" }
```
</details>

<details><summary>API Documentation</summary>

</details>