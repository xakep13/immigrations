USE [slavgorod.com.ua]
GO
/****** Object:  Table [dbo].[___test]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[___test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pos] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[advertising_interesting_facts]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertising_interesting_facts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Show] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[advertising_page]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertising_page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[advertising_statistic]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertising_statistic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[Statistic] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[author_blog]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[author_blog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Post] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Link] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[black_ip]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[black_ip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IP] [nvarchar](max) NULL,
	[Info] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[blog]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[blog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[CommentCount] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[IsTop] [int] NOT NULL,
	[ImageDirectory] [nvarchar](max) NOT NULL,
	[SpecialTheme_ID] [int] NULL,
	[Bloger_ID] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[blog__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[blog__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Blog_ID] [int] NOT NULL,
	[Comment_ID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bloger]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bloger](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Post] [nvarchar](max) NULL,
	[About] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NOT NULL,
	[BlogCount] [int] NOT NULL,
	[AddDate] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[buyandsell]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[buyandsell](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[AuthorID] [int] NULL,
	[AuthorName] [nvarchar](max) NULL,
	[Phones] [nvarchar](max) NULL,
	[CheckPhones] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[ShowEmail] [int] NULL,
	[Price] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[AddDateCheck] [int] NULL,
	[EndDateCheck] [int] NULL,
	[Photos] [nvarchar](max) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[SubCategory_ID] [int] NULL,
	[SubCategory_Title] [nvarchar](max) NULL,
	[SubCategory_UrlTitle] [nvarchar](max) NULL,
	[EditPassword] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[ViewCount] [int] NULL,
	[PhotoCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[buyandsell_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[buyandsell_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Position] [int] NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[buyandsell_subcategory]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[buyandsell_subcategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[ItemsCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Author_ID] [int] NOT NULL,
	[Author_Name] [nvarchar](max) NOT NULL,
	[Author_Image] [nvarchar](max) NULL,
	[Author_Link] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[AnonimLikes] [int] NOT NULL,
	[UserLikes] [int] NOT NULL,
	[Author_IP] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[contest]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Info] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL,
	[VotingStartDate] [datetime2](7) NULL,
	[VotingFinishDate] [datetime2](7) NULL,
	[IsTop] [int] NULL,
	[OneVoteOnly] [int] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[ItemShowType] [int] NULL,
	[AllowComments] [int] NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[contest__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contest__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Contest_ID] [nchar](10) NULL,
	[Comment_ID] [nchar](10) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[contest_item]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contest_item](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[ResultInfo] [nvarchar](max) NULL,
	[ContestItemCategory_ID] [int] NULL,
	[ContestItemCategory_Title] [nvarchar](max) NULL,
	[ContestItemCategory_Position] [int] NULL,
	[Contest_ID] [int] NULL,
	[ImageDirectory] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[contest_item_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contest_item_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Contest_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[contest_item_photo]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contest_item_photo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[ContestItem_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[digest]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[digest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[editors_choice]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[editors_choice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL,
	[ItemID] [int] NULL,
	[TypeID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[horoscope]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[horoscope](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sign] [int] NULL,
	[Type] [int] NULL,
	[Date] [datetime2](7) NULL,
	[Text] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[last_update]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[last_update](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastUpdate] [datetime2](7) NULL,
	[Type] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[logo]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[logo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[DateFrom] [datetime2](7) NULL,
	[DateTo] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news_article]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news_article](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[PhotoCount] [int] NOT NULL,
	[VideoCount] [int] NOT NULL,
	[AudioCount] [int] NOT NULL,
	[CommentCount] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[IsExclusive] [int] NULL,
	[IsPeople] [int] NULL,
	[IsEditorsChoice] [int] NULL,
	[IsCategoryTop] [int] NULL,
	[AllowComments] [int] NULL,
	[OnlyAuthUsers] [int] NULL,
	[HasPersons] [int] NULL,
	[TopPlace] [int] NULL,
	[InNewsLine] [int] NULL,
	[NewsLineStyle] [nvarchar](max) NULL,
	[AuthorName] [nvarchar](max) NULL,
	[AuthorPost] [nvarchar](max) NULL,
	[AuthorImage] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[Author_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[SpecialTheme_ID] [int] NULL,
	[SpecialTheme_Title] [nvarchar](max) NULL,
	[Questioning_ID] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[IsYandexNews] [int] NULL,
	[YandexGanre] [nvarchar](max) NULL,
 CONSTRAINT [PK_news_article] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news_article__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news_article__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NewsArticle_ID] [int] NOT NULL,
	[Comment_ID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news_article__person]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news_article__person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Person_FirstName] [nvarchar](max) NULL,
	[Person_LastName] [nvarchar](max) NULL,
	[Person_Image] [nvarchar](max) NULL,
	[Person_Post] [nvarchar](max) NULL,
	[Person_ID] [int] NULL,
	[NewsArticle_ID] [int] NULL,
	[NewsArticle_Title] [nvarchar](max) NULL,
	[NewsArticle_Date] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news_article_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news_article_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Position] [int] NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[person]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](500) NOT NULL,
	[LastName] [nvarchar](500) NULL,
	[SoName] [nvarchar](500) NULL,
	[Post] [nvarchar](max) NULL,
	[About] [nvarchar](max) NULL,
	[Info] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[PhotoCount] [int] NULL,
	[VideoCount] [int] NULL,
	[NewsCount] [int] NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[person_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[person_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Image] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photo]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photo](
	[ID] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Position] [int] NULL,
	[Date] [datetime2](7) NULL,
	[Rank] [int] NOT NULL,
	[Tags] [nvarchar](max) NULL,
	[PhotoGallery_ID] [int] NULL,
	[PhotoGallery_Name] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photo_gallery]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photo_gallery](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[AuthorName] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[PhotosCount] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photo_person]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photo_person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[Person_ID] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photoreport]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photoreport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[IsTop] [int] NULL,
	[IsEditorsChoice] [int] NULL,
	[ViewCount] [int] NULL,
	[PhotoCount] [int] NULL,
	[AuthorName] [nvarchar](max) NULL,
	[AuthorPost] [nvarchar](max) NULL,
	[AuthorImage] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[Author_ID] [int] NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photoreport_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photoreport_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Image] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photoreport_photo]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photoreport_photo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Item_ID] [int] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[AllowComments] [int] NULL,
	[OnlyAuthorizedUsers] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[photoreport_photo__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[photoreport_photo__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment_ID] [int] NULL,
	[PhotoreportPhoto_ID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pool]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pool](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[TotalRank] [int] NULL,
	[Priority] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pool_answer]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pool_answer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Answer] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Rank] [int] NULL,
	[Pool_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pravdomer]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pravdomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Statement] [nvarchar](max) NOT NULL,
	[AuthorName] [nvarchar](max) NOT NULL,
	[AuthorPost] [nvarchar](max) NULL,
	[AuthorImage] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[Rank] [int] NOT NULL,
	[TotalAnswersT] [int] NOT NULL,
	[TotalAnswersF] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[EndDate] [datetime2](7) NULL,
	[Comment] [nvarchar](max) NULL,
	[UserID] [int] NULL,
	[UserIP] [nvarchar](max) NULL,
	[VoteTimeCheck] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pravdomer_vote]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pravdomer_vote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Pravdomer_ID] [int] NULL,
	[User_ID] [int] NULL,
	[UserIP] [nvarchar](max) NULL,
	[VoteTimeCheck] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_account]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Profession] [nvarchar](max) NULL,
	[About] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Education] [nvarchar](max) NULL,
	[Experiance] [nvarchar](max) NULL,
	[WorkLocation] [nvarchar](max) NULL,
	[Contacts] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[AddDate] [datetime2](7) NULL,
	[PaymentDate] [datetime2](7) NULL,
	[ProfiPlan_ID] [int] NULL,
	[ProfiPlan_Title] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[PhotoCount] [int] NULL,
	[Rank] [float] NULL,
	[TotalFeedbackCount] [int] NULL,
	[PlusFeedbackCount] [int] NULL,
	[MinusFeedbackCount] [int] NULL,
	[NullFeedbackCount] [int] NULL,
	[ViewCount] [int] NULL,
	[TodayViewCount] [int] NULL,
	[YesterdayViewCount] [int] NULL,
	[CurrentMonthViewCount] [int] NULL,
	[LastMonthViewCount] [int] NULL,
	[SiteUser_ID] [int] NULL,
 CONSTRAINT [PK_profi_account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_account__feedback]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_account__feedback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Likes] [nvarchar](max) NULL,
	[Dislikes] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[Rank] [float] NULL,
	[Date] [datetime2](7) NULL,
	[SiteUser_Name] [nvarchar](max) NULL,
	[SiteUser_Image] [nvarchar](max) NULL,
	[SiteUser_IP] [nvarchar](max) NULL,
	[SiteUser_ID] [int] NULL,
	[Item_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_account__profi_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_account__profi_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProfiAccount_ID] [int] NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[Price] [int] NULL,
	[PriceType] [nvarchar](max) NULL,
	[PriceCurrency] [nvarchar](max) NULL,
	[Position] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_account__profi_speciality]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_account__profi_speciality](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProfiAccount_ID] [int] NOT NULL,
	[ProfiSpeciality_ID] [int] NOT NULL,
	[ProfiCategory_ID] [int] NULL,
 CONSTRAINT [PK_profi_account__profi_speciality] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_account_photo]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_account_photo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Item_ID] [int] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[AllowComments] [int] NULL,
	[OnlyAuthorizedUsers] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[ParentID] [int] NULL,
	[Level] [int] NULL,
	[ItemsCount] [int] NULL,
	[SearchWords] [nvarchar](max) NULL,
 CONSTRAINT [PK_profi_category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_faq]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_faq](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[Position] [int] NULL,
 CONSTRAINT [PK_profi_faq] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_plan]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_plan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Price] [int] NULL,
	[SpecialPrice] [int] NULL,
	[SpecialPriceDescription] [nvarchar](max) NULL,
	[PhotoCount] [int] NULL,
	[CategoryesCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[profi_speciality]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[profi_speciality](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
 CONSTRAINT [PK_profi_speciality] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[public_placemark]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[public_placemark](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Contacts] [nvarchar](max) NULL,
	[Coords] [nvarchar](max) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[public_placemark_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[public_placemark_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rating]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[AuthorName] [nvarchar](max) NOT NULL,
	[AuthorPost] [nvarchar](max) NULL,
	[AuthorImage] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[ViewCount] [int] NULL,
	[TopPlace] [int] NULL,
	[IsEditorsChoice] [int] NULL,
	[SpecialTheme_ID] [int] NULL,
	[Facts] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rating_item]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rating_item](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[About] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[ImageDirectory] [nvarchar](max) NULL,
	[Rating_ID] [int] NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[Rank] [int] NULL,
	[Facts] [nvarchar](max) NULL,
	[AllowComments] [int] NULL,
	[OnlyAuthorizedUsers] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[rating_item__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rating_item__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RatingItem_ID] [int] NOT NULL,
	[Comment_ID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[PayEndDate] [datetime2](7) NULL,
	[ViewCount] [int] NULL,
	[ViewCountQueue] [int] NULL,
	[PhotoCount] [int] NULL,
	[ServicePlan_ID] [int] NULL,
	[ServicePlan_Title] [nvarchar](max) NULL,
	[ServicePlan_Priority] [int] NULL,
	[Manager_ID] [int] NULL,
	[Client_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service__service_subcategory]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service__service_subcategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ItemID] [int] NULL,
	[SubCategory_ID] [int] NULL,
	[SubCategory_Title] [nvarchar](max) NULL,
	[SubCategory_UrlTitle] [nvarchar](max) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service_page]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[Color] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[ImageDirectory] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service_plan]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_plan](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Priority] [int] NULL,
	[SubCategoryCount] [int] NULL,
	[Price] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[service_subcategory]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[service_subcategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[Position] [int] NULL,
	[ItemsCount] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[site_user]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[site_user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Login] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[Blocked] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[site_user__role]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[site_user__role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteUser] [int] NULL,
	[Role] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[special_theme]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[special_theme](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Date] [datetime2](7) NOT NULL,
	[Priority] [int] NULL,
	[ShowInCarusel] [int] NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[static_page]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[static_page](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[ImageDirectory] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[temp_banner]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_banner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Banner] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Place] [int] NULL,
	[Client] [nvarchar](max) NULL,
	[DateFrom] [datetime2](7) NULL,
	[DateTo] [datetime2](7) NULL,
	[Link] [nvarchar](max) NULL,
	[BannerCode] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[temp_contextad]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[temp_contextad](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Place] [int] NULL,
	[Client] [nvarchar](max) NULL,
	[DateFrom] [datetime2](7) NULL,
	[DateTo] [datetime2](7) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[video_person]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[video_person](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[VideoCode] [nvarchar](max) NULL,
	[AddDate] [datetime2](7) NULL,
	[Person_ID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[videoreport]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[videoreport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Text] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[VideoID] [nvarchar](max) NULL,
	[ViewCount] [int] NULL,
	[CommentCount] [int] NULL,
	[AllowComments] [int] NULL,
	[OnlyAuthorizedUsers] [int] NULL,
	[IsTop] [int] NULL,
	[IsEditorsChoice] [int] NULL,
	[Category_ID] [int] NULL,
	[Category_Title] [nvarchar](max) NULL,
	[Category_UrlTitle] [nvarchar](max) NULL,
	[Author_ID] [int] NULL,
	[AuthorName] [nvarchar](max) NULL,
	[AuthorPost] [nvarchar](max) NULL,
	[AuthorImage] [nvarchar](max) NULL,
	[AuthorLink] [nvarchar](max) NULL,
	[Date] [datetime2](7) NULL,
	[MetaTitle] [nvarchar](max) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[videoreport__comment]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[videoreport__comment](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Comment_ID] [int] NULL,
	[Videoreport_ID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[videoreport_category]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[videoreport_category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[UrlTitle] [nvarchar](max) NULL,
	[Image] [nvarchar](max) NULL,
	[Position] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[vote_contest]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[vote_contest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteUser_ID] [int] NULL,
	[ContestItem_ID] [int] NULL,
	[Contest_ID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[weather]    Script Date: 08.02.2017 15:45:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[weather](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NULL,
	[Hour] [int] NULL,
	[Cloud] [nvarchar](max) NULL,
	[CloudImage] [nvarchar](max) NULL,
	[PrecipitationProbability] [int] NULL,
	[MinTemperature] [nvarchar](max) NULL,
	[MaxTemperature] [nvarchar](max) NULL,
	[MinPressure] [nvarchar](max) NULL,
	[MaxPressure] [nvarchar](max) NULL,
	[MinWindSpeed] [nvarchar](max) NULL,
	[MaxWindSpeed] [nvarchar](max) NULL,
	[WindDirection] [nvarchar](max) NULL,
	[MinHumidity] [nvarchar](max) NULL,
	[MaxHumidity] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[pool] ADD  CONSTRAINT [DF_pool_TotalRank]  DEFAULT ((0)) FOR [TotalRank]
GO
ALTER TABLE [dbo].[pool_answer] ADD  CONSTRAINT [DF_pool_answer_Rank]  DEFAULT ((0)) FOR [Rank]
GO
