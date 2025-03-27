CREATE TABLE [dbo].[tblFamilyTreeSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](400) NULL,
	[MiddleName] [nvarchar](400) NULL,
	[LastName] [nvarchar](400) NULL,
	[DOB] [datetime] NULL,
	[Age] [int] NULL,
	[Gender] [int] NULL,
	[Occupation] [int] NULL,
	[FatherName] [int] NULL,
	[MotherName] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[DeathDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[MatrialStatus] [int] NULL,
	[NumberOfChildren] [int] NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[WIfeId] [int] NULL,
	[HusbandId] [int] NULL,
	[GenerationType] [int] NULL,
	[PhoneNumber] [varchar](15) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblFamilyTreeSetup]  WITH CHECK ADD FOREIGN KEY([Gender])
REFERENCES [dbo].[tblGender] ([Id])
GO

ALTER TABLE [dbo].[tblFamilyTreeSetup]  WITH CHECK ADD FOREIGN KEY([Occupation])
REFERENCES [dbo].[tblOccupation] ([Id])
GO

ALTER TABLE [dbo].[tblFamilyTreeSetup]  WITH CHECK ADD  CONSTRAINT [FK_tblfamilytree_Matrialstatus] FOREIGN KEY([MatrialStatus])
REFERENCES [dbo].[tblMatrialStatus] ([Id])
GO

ALTER TABLE [dbo].[tblFamilyTreeSetup] CHECK CONSTRAINT [FK_tblfamilytree_Matrialstatus]
GO