USE [master]
GO
/****** Object:  Database [Message]    Script Date: 03.07.2017 13:25:30 ******/
CREATE DATABASE [Message]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DocsVisionDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\DocsVisionDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DocsVisionDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\DocsVisionDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Message] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Message].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Message] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Message] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Message] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Message] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Message] SET ARITHABORT OFF 
GO
ALTER DATABASE [Message] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Message] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Message] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Message] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Message] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Message] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Message] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Message] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Message] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Message] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Message] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Message] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Message] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Message] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Message] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Message] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Message] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Message] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Message] SET  MULTI_USER 
GO
ALTER DATABASE [Message] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Message] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Message] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Message] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Message] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Message] SET QUERY_STORE = OFF
GO
USE [Message]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Message]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [uniqueidentifier] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[surname] [nvarchar](50) NOT NULL,
	[department] [nvarchar](50) NULL,
	[position] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Select_user_info_by_id]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description: Получить информацию о пользователе
-- =============================================
CREATE FUNCTION [dbo].[uf_Select_user_info_by_id] 
(	
	@id uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 

		id
		, name
		, surname
		, department
		, position
		, email

	FROM [dbo].[User] WITH (nolock)

	WHERE id = @id
)

GO
/****** Object:  Table [dbo].[Message]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[id] [uniqueidentifier] NOT NULL,
	[theme] [nvarchar](30) NULL,
	[date] [datetime] NOT NULL,
	[text] [nvarchar](max) NOT NULL,
	[senderId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Select_message_info_by_id]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description: Получить информацию о сообщении
-- =============================================
CREATE FUNCTION [dbo].[uf_Select_message_info_by_id] 
(	
	@id uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 

		id
		, theme
		, date
		, text
		, senderId

	FROM [dbo].[Message] WITH (nolock)

	WHERE id = @id
)

GO
/****** Object:  Table [dbo].[UserMessage]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMessage](
	[userId] [uniqueidentifier] NOT NULL,
	[messageId] [uniqueidentifier] NOT NULL,
	[isRead] [bit] NOT NULL,
 CONSTRAINT [PK_UserMessage] PRIMARY KEY CLUSTERED 
(
	[userId] ASC,
	[messageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Select_message_info_by_userid]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description: Получить все сообщения пользователя по идентификатору пользователя
-- =============================================
CREATE FUNCTION [dbo].[uf_Select_message_info_by_userid] 
(	
	@userId uniqueidentifier
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 

		userId
		, messageId
		, isRead

	FROM [dbo].[UserMessage] WITH (nolock)

	WHERE userId = @userId
)

GO
/****** Object:  UserDefinedFunction [dbo].[uf_Select_user_info_by_email]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description: Получить информацию о пользователе по email
-- =============================================
CREATE FUNCTION [dbo].[uf_Select_user_info_by_email] 
(	
	@email nvarchar(50)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 

		id
		, name
		, surname
		, department
		, position
		, email

	FROM [dbo].[User] WITH (nolock)

	WHERE email = @email
)

GO
/****** Object:  Index [IX_Sender]    Script Date: 03.07.2017 13:25:30 ******/
CREATE NONCLUSTERED INDEX [IX_Sender] ON [dbo].[Message]
(
	[senderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_User_Message] FOREIGN KEY([senderId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_User_Message]
GO
ALTER TABLE [dbo].[UserMessage]  WITH CHECK ADD  CONSTRAINT [FK_UserMessage_Message] FOREIGN KEY([messageId])
REFERENCES [dbo].[Message] ([id])
GO
ALTER TABLE [dbo].[UserMessage] CHECK CONSTRAINT [FK_UserMessage_Message]
GO
ALTER TABLE [dbo].[UserMessage]  WITH CHECK ADD  CONSTRAINT [FK_UserMessage_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserMessage] CHECK CONSTRAINT [FK_UserMessage_User]
GO
/****** Object:  StoredProcedure [dbo].[up_Delete_user_messages]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description:	Удаление сообщений пользователя.
-- =============================================
CREATE PROCEDURE [dbo].[up_Delete_user_messages] 
(
	@userId uniqueidentifier,
	@jsonIds varchar(max)
)
AS
BEGIN

	DELETE UM FROM dbo.UserMessage UM
	WHERE UM.userId = @userId AND UM.messageId IN (SELECT value FROM OPENJSON(@jsonIds))
   
END

GO
/****** Object:  StoredProcedure [dbo].[up_Send_new_Message]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description:	Отправка сообщения пользователям
-- =============================================
CREATE PROCEDURE [dbo].[up_Send_new_Message]
(
	@jsonIds nvarchar(max),
	@messageId uniqueidentifier,
	@theme nvarchar(30),
	@date datetime,
	@text nvarchar(max),
	@senderId uniqueidentifier	 
)
AS
BEGIN

	BEGIN TRANSACTION
	 
	INSERT INTO dbo.[Message]
	(
		id
		, theme
		, date
		, text
		, senderId
	) 
	VALUES 
	(
		@messageId
		, @theme
		, @date
		, @text
		, @senderId
	)

	INSERT INTO dbo.[UserMessage]
	(
		userId
		, messageId
		, isRead
	)
	SELECT value, @messageId, 0 FROM OPENJSON(@jsonIds)

	COMMIT
END

GO
/****** Object:  StoredProcedure [dbo].[up_Update_read_in_message]    Script Date: 03.07.2017 13:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Богомолов Р.В.
-- Description:	Сообщение прочитано
-- =============================================
CREATE PROCEDURE [dbo].[up_Update_read_in_message]
(
	@messageId uniqueidentifier,
	@userId uniqueidentifier
)
AS
BEGIN
	
	UPDATE dbo.[UserMessage] with(rowlock)
	SET isRead = 1
	WHERE messageId = @messageId AND userId = @userId

END

GO
USE [master]
GO
ALTER DATABASE [Message] SET  READ_WRITE 
GO
