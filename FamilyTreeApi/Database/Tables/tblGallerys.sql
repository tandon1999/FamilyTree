CREATE TABLE [dbo].[tblGallerys](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhotoName] [nvarchar](400) NULL,
	[Category] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[DateofPhoto] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[ImagePath] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO