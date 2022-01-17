#MusicLog 

# Welcome to the MusicLog, your Music Store API

## Requirements
* 1- Microsoft .Net 6.0 (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* 2- Microsoft Sql Server express or other versions (https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)    
* [optional] 3- Microsoft Visual Studio 2022 Express or other versions (https://visualstudio.microsoft.com/pt-br/vs/)
* [optional] 4- Windows Terminal or other terminal app (https://www.microsoft.com/pt-br/p/windows-terminal/9n0dx20hk701?activetab=pivot:overviewtab)

## Installation
### 1- Create the Databsase 
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
### 2- Configure the Application
Into the folder `src\Musicalog.Api\appsettings.json`  alter the ConnectionString to configure for you environment,
For example:
```yaml
"ConnectionStrings": { "MusicLogDb": "Data Source=.;Initial Catalog=MusicLogDB;Persist Security Info=True;User ID=SA;Password=1q2w3e4r!Q@W#E" }
```

### 3- Compile and Run the application
At this moment you can compile and run the application using the Visual Studio or using the .Net Command-line following the steps below.

Using the Windows Terminal or other, navigate to `src\Musicalog.Api` folder and run the command

```console
dotnet build
```
and then
```console
dotnet run
```

In both choices, the application will start and be listen to by https://localhost:7155 address.
Also, you can access the swagger documentation in the address: https://localhost:7155/swagger/index.html


## API Documentation
Follow below some examples to create, retrieve, update, delete and filter the Artists and Album data from the respective endpoints. 

### Artists Endpoint
#### List
> Endpoint => https://localhost:7155/api/v1/Artists | Method => Get
#### Retrieve
> Endpoint => https://localhost:7155/api/v1/Artists/{id} | Method => Get
#### Create
> Endpoint => https://localhost:7155/api/v1/Artists | Method => POST
```json
{
    "Name": "Brian may"
}
```
#### Update
> Endpoint => https://localhost:7155/api/v1/Artists/{id} | Method => PUT
```json
{
    "Name": "Freddie Mercury"
}
```
#### Delete
> Endpoint => https://localhost:7155/api/v1/Artists/{id} | Method => DELETE

### Albums Endpoint
#### List
> Endpoint => https://localhost:7155/api/v1/Albums | Method => Get
### Filter
The filter can be made by Artist Name or Album Name using the query string parameters
> https://localhost:7155/api/v1/Albums?Artist=Fred

or

> https://localhost:7155/api/v1/Albums?Album=inn

or both

> https://localhost:7155/api/v1/Albums?Artist=Fred&Album=Inn
#### Retrieve
> Endpoint => https://localhost:7155/api/v1/Albums/{id} | Method => Get
#### Create
> Endpoint => https://localhost:7155/api/v1/Albums | Method => POST
```json
{
    "Title": "Sunday Blood Sunday",
    "AlbumType": "CD",
    "Artists": [1,2,3],
    "Stock": true
}
```
the `Artists` receives an array list of int with artists Identifiers (id)
#### Update
> Endpoint => https://localhost:7155/api/v1/Albums/{id} | Method => PUT
```json
{
    "Title": "Sunday Blood Sunday",
    "AlbumType": "CD",
    "Artists": [1,2,3],
    "Stock": true
}
```
the `Artists` receives an array list of int with artists Identifiers (id)
#### Delete
> Endpoint => https://localhost:7155/api/v1/Albums/{id} | Method => DELETE

---
That's it, I hope that you enjoy the application, if you have and question, problem or just want to say hello! feel free to contact me at olavo.o.neto@gmail.com

Thank you!! :)
