USE [master]
GO
/****** Object:  Database [DBTomocaUpdate]    Script Date: 7/27/2021 6:16:33 PM ******/
CREATE DATABASE [DBTomocaUpdate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBTomocaUpdate', FILENAME = N'D:\dbTomoca\DBTomocaUpdate.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBTomocaUpdate_log', FILENAME = N'D:\dbTomoca\DBTomocaUpdate_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBTomocaUpdate] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBTomocaUpdate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBTomocaUpdate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBTomocaUpdate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBTomocaUpdate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBTomocaUpdate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBTomocaUpdate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBTomocaUpdate] SET  MULTI_USER 
GO
ALTER DATABASE [DBTomocaUpdate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBTomocaUpdate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBTomocaUpdate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBTomocaUpdate] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBTomocaUpdate] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBTomocaUpdate] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBTomocaUpdate] SET QUERY_STORE = OFF
GO
USE [DBTomocaUpdate]
GO
/****** Object:  Table [dbo].[Actors]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actors](
	[ActorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Nationality] [nvarchar](100) NOT NULL,
	[Birth] [date] NULL,
 CONSTRAINT [PK_Actors] PRIMARY KEY CLUSTERED 
(
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[MovieID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Directors]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Directors](
	[DirectorID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Nationality] [nvarchar](100) NOT NULL,
	[Birth] [date] NULL,
 CONSTRAINT [PK_Directors] PRIMARY KEY CLUSTERED 
(
	[DirectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreID] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GenreName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GenreName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MiAnLien]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MiAnLien](
	[MalID] [int] IDENTITY(1,1) NOT NULL,
	[MovieID] [int] NOT NULL,
	[TheaterID] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[A4] [char](5) NULL,
	[A5] [char](5) NULL,
	[A6] [char](5) NULL,
	[A7] [char](5) NULL,
	[A8] [char](5) NULL,
	[A9] [char](5) NULL,
	[A10] [char](5) NULL,
	[A11] [char](5) NULL,
	[A12] [char](5) NULL,
	[A13] [char](5) NULL,
	[B2] [char](5) NULL,
	[B3] [char](5) NULL,
	[B4] [char](5) NULL,
	[B5] [char](5) NULL,
	[B6] [char](5) NULL,
	[B7] [char](5) NULL,
	[B8] [char](5) NULL,
	[B9] [char](5) NULL,
	[B10] [char](5) NULL,
	[B11] [char](5) NULL,
	[B12] [char](5) NULL,
	[B13] [char](5) NULL,
	[B14] [char](5) NULL,
	[B15] [char](5) NULL,
	[C2] [char](5) NULL,
	[C3] [char](5) NULL,
	[C4] [char](5) NULL,
	[C5] [char](5) NULL,
	[C6] [char](5) NULL,
	[C7] [char](5) NULL,
	[C8] [char](5) NULL,
	[C9] [char](5) NULL,
	[C10] [char](5) NULL,
	[C11] [char](5) NULL,
	[C12] [char](5) NULL,
	[C13] [char](5) NULL,
	[C14] [char](5) NULL,
	[C15] [char](5) NULL,
	[D2] [char](5) NULL,
	[D3] [char](5) NULL,
	[D4] [char](5) NULL,
	[D5] [char](5) NULL,
	[D6] [char](5) NULL,
	[D7] [char](5) NULL,
	[D8] [char](5) NULL,
	[D9] [char](5) NULL,
	[D10] [char](5) NULL,
	[D11] [char](5) NULL,
	[D12] [char](5) NULL,
	[D13] [char](5) NULL,
	[D14] [char](5) NULL,
	[D15] [char](5) NULL,
	[E2] [char](5) NULL,
	[E3] [char](5) NULL,
	[E4] [char](5) NULL,
	[E5] [char](5) NULL,
	[E6] [char](5) NULL,
	[E7] [char](5) NULL,
	[E8] [char](5) NULL,
	[E9] [char](5) NULL,
	[E10] [char](5) NULL,
	[E11] [char](5) NULL,
	[E12] [char](5) NULL,
	[E13] [char](5) NULL,
	[E14] [char](5) NULL,
	[E15] [char](5) NULL,
	[F1] [char](5) NULL,
	[F2] [char](5) NULL,
	[F3] [char](5) NULL,
	[F4] [char](5) NULL,
	[F5] [char](5) NULL,
	[F6] [char](5) NULL,
	[F7] [char](5) NULL,
	[F8] [char](5) NULL,
	[F9] [char](5) NULL,
	[F10] [char](5) NULL,
	[F11] [char](5) NULL,
	[F12] [char](5) NULL,
	[F13] [char](5) NULL,
	[F14] [char](5) NULL,
	[F15] [char](5) NULL,
	[F16] [char](5) NULL,
 CONSTRAINT [PK_MiAnLien] PRIMARY KEY CLUSTERED 
(
	[MalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieActor]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieActor](
	[MovieID] [int] NOT NULL,
	[ActorID] [int] NOT NULL,
 CONSTRAINT [PK_MovieActor] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieDirector]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieDirector](
	[MovieID] [int] NOT NULL,
	[DirectorID] [int] NOT NULL,
 CONSTRAINT [PK_MovieDirector] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[DirectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NULL,
	[Plot] [varchar](max) NULL,
	[ReleaseYear] [int] NULL,
	[IMDb] [float] NULL,
	[Tomatometer] [varchar](10) NULL,
	[AudienceScore] [varchar](10) NULL,
	[Image] [varchar](max) NULL,
	[Trailer] [varchar](max) NULL,
	[TimeOfFilm] [time](7) NULL,
	[Banner] [nvarchar](max) NULL,
	[ComingSoon] [int] NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MoviesGenres]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoviesGenres](
	[MovieID] [int] NOT NULL,
	[GenreID] [int] NOT NULL,
 CONSTRAINT [PK_MoviesGenres] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieTheater]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieTheater](
	[TheaterID] [int] IDENTITY(1,1) NOT NULL,
	[TheaterName] [nvarchar](max) NOT NULL,
	[CityID] [int] NOT NULL,
	[TypeID] [int] NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_MovieTheater] PRIMARY KEY CLUSTERED 
(
	[TheaterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewsID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Title] [nvarchar](max) NULL,
	[Image1] [nvarchar](max) NULL,
	[Content1] [nvarchar](max) NULL,
	[Image2] [nvarchar](max) NULL,
	[Content2] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[Photo] [nvarchar](max) NULL,
	[ReadCount] [money] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReviewOfMovie]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReviewOfMovie](
	[MovieID] [int] NOT NULL,
	[YoutubeID] [int] NOT NULL,
 CONSTRAINT [PK_ReviewOfMovie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[YoutubeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Theater]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Theater](
	[TypeID] [int] IDENTITY(1,1) NOT NULL,
	[Theater] [varchar](50) NOT NULL,
	[Logo] [nvarchar](max) NULL,
 CONSTRAINT [PK_Theater] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Theater] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketID] [int] IDENTITY(1, 1) NOT NULL,
	[MalID] [int] NOT NULL,
	[Money] [float] NOT NULL,
	[Seat] [nvarchar](500) NULL,
	[Vip] [int] NULL,
	[Normal] [int] NULL,
	[UserID] [int] NOT NULL,
	[AmountSeats] [int] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Permission] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YoutubeReviews]    Script Date: 7/27/2021 6:16:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YoutubeReviews](
	[YoutubeID] [int] IDENTITY(1,1) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Video] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_YoutubeReviews] PRIMARY KEY CLUSTERED 
(
	[YoutubeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Movies]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[MiAnLien]  WITH CHECK ADD  CONSTRAINT [FK_MiAnLien_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[MiAnLien] CHECK CONSTRAINT [FK_MiAnLien_Movies]
GO
ALTER TABLE [dbo].[MiAnLien]  WITH CHECK ADD  CONSTRAINT [FK_MiAnLien_MovieTheater] FOREIGN KEY([TheaterID])
REFERENCES [dbo].[MovieTheater] ([TheaterID])
GO
ALTER TABLE [dbo].[MiAnLien] CHECK CONSTRAINT [FK_MiAnLien_MovieTheater]
GO
ALTER TABLE [dbo].[MovieActor]  WITH CHECK ADD  CONSTRAINT [FK_MovieActor_Actors] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actors] ([ActorID])
GO
ALTER TABLE [dbo].[MovieActor] CHECK CONSTRAINT [FK_MovieActor_Actors]
GO
ALTER TABLE [dbo].[MovieActor]  WITH CHECK ADD  CONSTRAINT [FK_MovieActor_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[MovieActor] CHECK CONSTRAINT [FK_MovieActor_Movies]
GO
ALTER TABLE [dbo].[MovieDirector]  WITH CHECK ADD  CONSTRAINT [FK_MovieDirector_Directors] FOREIGN KEY([DirectorID])
REFERENCES [dbo].[Directors] ([DirectorID])
GO
ALTER TABLE [dbo].[MovieDirector] CHECK CONSTRAINT [FK_MovieDirector_Directors]
GO
ALTER TABLE [dbo].[MovieDirector]  WITH CHECK ADD  CONSTRAINT [FK_MovieDirector_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[MovieDirector] CHECK CONSTRAINT [FK_MovieDirector_Movies]
GO
ALTER TABLE [dbo].[MoviesGenres]  WITH CHECK ADD  CONSTRAINT [FK_MoviesGenres_Genres] FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genres] ([GenreID])
GO
ALTER TABLE [dbo].[MoviesGenres] CHECK CONSTRAINT [FK_MoviesGenres_Genres]
GO
ALTER TABLE [dbo].[MoviesGenres]  WITH CHECK ADD  CONSTRAINT [FK_MoviesGenres_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[MoviesGenres] CHECK CONSTRAINT [FK_MoviesGenres_Movies]
GO
ALTER TABLE [dbo].[MovieTheater]  WITH CHECK ADD  CONSTRAINT [FK_MovieTheater_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[MovieTheater] CHECK CONSTRAINT [FK_MovieTheater_City]
GO
ALTER TABLE [dbo].[MovieTheater]  WITH CHECK ADD  CONSTRAINT [FK_MovieTheater_Theater] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Theater] ([TypeID])
GO
ALTER TABLE [dbo].[MovieTheater] CHECK CONSTRAINT [FK_MovieTheater_Theater]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_Users]
GO
ALTER TABLE [dbo].[ReviewOfMovie]  WITH CHECK ADD  CONSTRAINT [FK_ReviewOfMovie_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[ReviewOfMovie] CHECK CONSTRAINT [FK_ReviewOfMovie_Movies]
GO
ALTER TABLE [dbo].[ReviewOfMovie]  WITH CHECK ADD  CONSTRAINT [FK_ReviewOfMovie_YoutubeReviews] FOREIGN KEY([YoutubeID])
REFERENCES [dbo].[YoutubeReviews] ([YoutubeID])
GO
ALTER TABLE [dbo].[ReviewOfMovie] CHECK CONSTRAINT [FK_ReviewOfMovie_YoutubeReviews]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_MiAnLien] FOREIGN KEY([MalID])
REFERENCES [dbo].[MiAnLien] ([MalID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_MiAnLien]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Users]
GO
USE [master]
GO
ALTER DATABASE [DBTomocaUpdate] SET  READ_WRITE 
GO
