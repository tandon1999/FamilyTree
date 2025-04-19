SET IDENTITY_INSERT [Auth].[tblRoles] ON 
GO
INSERT [Auth].[tblRoles] ([RoleId], [RoleName], [IsActive], [CreatedDate]) VALUES (1, N'Admin', 1, CAST(N'2025-04-19T09:16:22.430' AS DateTime))
GO
INSERT [Auth].[tblRoles] ([RoleId], [RoleName], [IsActive], [CreatedDate]) VALUES (2, N'User', 1, CAST(N'2025-04-19T09:16:22.430' AS DateTime))
GO
SET IDENTITY_INSERT [Auth].[tblRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON 
GO
INSERT [dbo].[tblUsers] ([UserId], [UserName], [Password], [Email], [RoleId]) VALUES (1, N'Suman', N'I9Tql9uqyNSgRnoI+dzUJHrd9GbfAvaICUhVRhXjbD4=', N'sumantandan1999@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
GO