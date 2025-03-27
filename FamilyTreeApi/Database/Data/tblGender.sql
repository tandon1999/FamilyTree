--USE [DB_FamilyTree]
--GO

SET IDENTITY_INSERT [dbo].[tblGender] ON 
GO
INSERT [dbo].[tblGender] ([Id], [GenderName]) VALUES (1, N'पुरुष')
GO
INSERT [dbo].[tblGender] ([Id], [GenderName]) VALUES (2, N'महिला')
GO
INSERT [dbo].[tblGender] ([Id], [GenderName]) VALUES (3, N'अन्य')
GO
SET IDENTITY_INSERT [dbo].[tblGender] OFF
GO



