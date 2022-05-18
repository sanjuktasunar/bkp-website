
GO
DROP TABLE [dbo].[Municipality]
GO

GO
DROP TABLE [dbo].[District]
GO


GO
DROP TABLE [dbo].[Province]
GO


GO
CREATE TABLE [dbo].[Province]
(
	[ProvinceId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [Province_PK] PRIMARY KEY,
	[ProvinceName] [nvarchar](200) NULL,
	[Status] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
);
GO

GO
CREATE UNIQUE INDEX Province_ProvinceName_ui ON 
[dbo].[Province](ProvinceName)
GO



GO
SET IDENTITY_INSERT [dbo].[Province] ON 
GO

GO
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (1, N'Province 1', 1, CAST(N'2020-05-26T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (2, N'Province 2', 1, CAST(N'2020-05-26T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (3, N'Province 3', 1, CAST(N'2020-05-26T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (6, N'Province 4', 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (7, N'Province 5', 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (8, N'Province 6', 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Province] ([ProvinceId], [ProvinceName], [Status], [CreatedDate], [CreatedBy]) VALUES (9, N'Province 7', 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL)
GO

GO
SET IDENTITY_INSERT [dbo].[Province] OFF
GO



GO
CREATE TABLE [dbo].[District]
(
	[DistrictId] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [District _PK] PRIMARY KEY,
	[DistrictName] [nvarchar](200) NOT NULL,
	[DistrictCode] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ProvinceId] [int] NOT NULL CONSTRAINT District_Province_ProvinceId References Province(ProvinceId),
	Status bit null default(1),
	CreatedBy int null Constraint District_Users_CreatedBy_fk References Users(UserId),
	CreatedDate datetime null,
	UpdatedBy int null Constraint District_Users_UpdatedBy_fk References Users(UserId),
	UpdatedDate datetime null
 );
GO

GO
CREATE UNIQUE INDEX District_DistrictName_ui ON
[dbo].[District](DistrictName,ProvinceId)
GO
GO
CREATE UNIQUE INDEX District_DistrictCode_ui ON
[dbo].[District](DistrictCode,ProvinceId)
GO

GO
SET IDENTITY_INSERT [dbo].[District] ON 
GO


GO
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (4, N'Bhojpur', 1035, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (5, N'Dhankuta', 1036, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (6, N'Ilam', 1051, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (7, N'Jhapa', 1052, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (8, N'Khotang', 1065, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (9, N'Morang', 1037, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (10, N'Okhaldhunga', 1066, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (11, N'Panchthar', 1053, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (12, N'Sankhuwasabha', 1038, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (13, N'Solukhumbu', 1069, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (14, N'Sunsari', 1039, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (15, N'Taplejung', 1054, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (16, N'Terathum', 1040, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (17, N'Udayapur', 1070, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (33, N'Parsa', 1058, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (34, N'Bara', 1055, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (35, N'Rautahat', 1059, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (36, N'Sarlahi', 1028, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (37, N'Dhanusa', 1024, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (38, N'Siraha', 1068, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (39, N'Mahottari', 1026, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 2)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (42, N'Sindhuli', 1029, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (43, N'Ramechhap', 1027, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (44, N'Dolkha', 1025, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (45, N'Bhaktapur', 1001, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (46, N'Dhading', 1002, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (47, N'Kathmandu', 1003, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (48, N'Kavrepalanchowk', 1004, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (49, N'Lalitpur', 1005, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (50, N'Nuwakot', 1006, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (51, N'Rasuwa', 1007, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (52, N'Sindhupalchok', 1008, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (53, N'Chitwan', 1056, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (54, N'Makwanpur', 1057, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 3)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (56, N'Baglung', 1014, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (57, N'Gorkha', 1018, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (58, N'Kaski', 1019, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (59, N'Lamjung', 1020, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (60, N'Manang', 1021, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (61, N'Mustang', 1015, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (62, N'Myagdi', 1016, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (63, N'Nawalparasi', 1044, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (64, N'Parbat', 1017, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (65, N'Syangja', 1022, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (66, N'Tanahu', 1023, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 6)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (68, N'Kapilvastu', 1043, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (69, N'Parsa', 1058, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (70, N'Rupandehi', 1046, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (71, N'Arghakhanchi', 1041, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (72, N'Gulmi', 1042, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (73, N'Palpa', 1045, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (74, N'Dang', 1060, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (75, N'Pyuthan', 1061, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (76, N'Rolpa', 1062, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (77, N'Rukum', 1063, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (78, N'Banke', 1009, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (79, N'Bardiya', 1010, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 7)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (81, N'Rukum', 1063, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (82, N'Salyan', 1064, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (83, N'Dolpa', 1030, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (84, N'Humla', 1031, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (85, N'Jumla', 1032, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (86, N'Kalikot', 1033, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (87, N'Mugu', 1034, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (88, N'Surketh', 1013, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (89, N'Dailekh', 1011, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (90, N'Jajarkot', 1012, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 8)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (92, N'Kailali', 1075, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (93, N'Achham', 1071, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (94, N'Doti', 1074, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (95, N'Bajhang', 1072, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (96, N'Bajura', 1073, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (97, N'Kanchanpur', 1050, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (98, N'Dadeldhura', 1048, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (99, N'Baitidi', 1047, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
INSERT [dbo].[District] ([DistrictId], [DistrictName], [DistrictCode], [IsActive], [CreatedDate], [CreatedBy], [ProvinceId]) VALUES (100, N'Darchula', 1049, 1, CAST(N'2020-05-27T00:00:00.0000000' AS DateTime2), NULL, 9)
GO

GO
SET IDENTITY_INSERT [dbo].[District] OFF
GO
