USE [MyFirstGame]
GO
/****** Object:  Table [dbo].[characters]    Script Date: 03/17/2015 16:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[characters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[heroName] [varchar](50) NOT NULL,
	[charLevel] [int] NOT NULL,
	[exp] [int] NOT NULL,
	[games] [int] NOT NULL,
	[win] [int] NOT NULL,
	[score] [int] NOT NULL,
 CONSTRAINT [PK_characters] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_characters] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[characters] ON
INSERT [dbo].[characters] ([id], [account], [name], [heroName], [charLevel], [exp], [games], [win], [score]) VALUES (45, N'stas51447d', N'ZeT', N'Эйлад', 8, 15035, 310, 173, 370860)
INSERT [dbo].[characters] ([id], [account], [name], [heroName], [charLevel], [exp], [games], [win], [score]) VALUES (53, N'stas51447', N'Sacura', N'Саисса', 7, 11185, 304, 138, 490737)
INSERT [dbo].[characters] ([id], [account], [name], [heroName], [charLevel], [exp], [games], [win], [score]) VALUES (69, N'bill', N'423423', N'Валадор', 3, 902, 7, 0, 469)
SET IDENTITY_INSERT [dbo].[characters] OFF
/****** Object:  Table [dbo].[characterTemplate]    Script Date: 03/17/2015 16:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[characterTemplate](
	[heroCards] [int] NOT NULL,
	[firstCard] [int] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (1, 5)
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (2, 6)
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (3, 7)
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (4, 5)
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (8, 6)
INSERT [dbo].[characterTemplate] ([heroCards], [firstCard]) VALUES (12, 7)
/****** Object:  Table [dbo].[characterCards]    Script Date: 03/17/2015 16:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[characterCards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[charId] [int] NOT NULL,
	[cardId] [int] NOT NULL,
	[slot] [int] NOT NULL,
 CONSTRAINT [PK_characterCards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[characterCards] ON
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (27, 45, 3, 0)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (29, 53, 8, 0)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (72, 53, 6, 1)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (222, 45, 6, 1)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (503, 45, 5, 2)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (504, 45, 9, 4)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (505, 53, 11, 4)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (506, 45, 6, 3)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (507, 53, 5, 3)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (508, 53, 11, 2)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (509, 45, 9, 5)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (510, 45, 5, 6)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (511, 45, 11, 7)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (512, 53, 9, 5)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (513, 45, 6, 10)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (514, 45, 10, 11)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (515, 53, 7, 10)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (516, 45, 9, 12)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (517, 45, 10, 13)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (518, 45, 9, 14)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (519, 45, 10, 15)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (520, 45, 10, 16)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (521, 45, 7, 17)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (522, 45, 11, 18)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (523, 45, 10, 19)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (524, 45, 7, 20)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (525, 45, 11, 21)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (526, 45, 11, 22)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (527, 45, 6, 23)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (528, 45, 10, 24)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (529, 45, 9, 25)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (530, 45, 9, 26)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (531, 45, 9, 27)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (532, 45, 9, 28)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (533, 45, 10, 29)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (534, 45, 11, 30)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (535, 45, 11, 31)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (536, 45, 6, 32)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (537, 45, 9, 33)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (538, 45, 5, 34)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (539, 45, 5, 35)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (540, 45, 9, 36)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (541, 45, 6, 37)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (542, 45, 5, 38)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (543, 45, 10, 39)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (544, 45, 10, 40)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (545, 45, 7, 41)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (546, 45, 9, 42)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (547, 45, 11, 43)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (548, 45, 5, 44)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (549, 45, 10, 45)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (550, 45, 11, 46)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (551, 45, 5, 47)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (552, 45, 11, 48)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (553, 45, 5, 49)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (554, 45, 10, 50)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (555, 45, 9, 51)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (556, 45, 10, 52)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (557, 45, 9, 53)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (558, 45, 9, 54)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (559, 45, 7, 55)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (560, 45, 11, 56)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (561, 45, 6, 57)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (562, 45, 10, 58)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (563, 45, 6, 59)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (564, 45, 6, 60)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (565, 45, 5, 61)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (566, 45, 10, 62)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (567, 45, 11, 63)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (568, 45, 10, 64)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (569, 45, 11, 65)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (570, 45, 5, 66)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (571, 45, 10, 67)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (572, 45, 9, 68)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (573, 45, 18, 69)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (574, 45, 21, 70)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (575, 45, 13, 71)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (576, 45, 23, 72)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (577, 45, 22, 73)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (578, 45, 10, 74)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (579, 45, 25, 75)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (580, 45, 21, 76)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (581, 45, 15, 77)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (582, 45, 11, 78)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (583, 45, 16, 79)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (584, 45, 20, 80)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (585, 45, 24, 81)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (586, 45, 16, 82)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (587, 45, 13, 83)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (588, 45, 25, 84)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (589, 45, 15, 85)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (590, 45, 21, 86)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (591, 45, 24, 87)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (592, 45, 25, 88)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (593, 45, 23, 89)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (594, 45, 23, 90)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (595, 45, 21, 91)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (596, 45, 14, 92)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (597, 45, 13, 93)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (598, 45, 16, 94)
GO
print 'Processed 100 total records'
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (599, 45, 21, 95)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (600, 45, 18, 96)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (601, 45, 25, 97)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (602, 45, 15, 98)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (603, 45, 18, 99)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (604, 45, 16, 100)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (605, 45, 9, 101)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (606, 45, 22, 102)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (607, 45, 14, 103)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (608, 45, 23, 104)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (609, 45, 11, 105)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (610, 45, 20, 106)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (611, 45, 22, 107)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (612, 45, 25, 108)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (613, 45, 19, 109)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (614, 45, 17, 110)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (615, 45, 24, 111)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (616, 45, 22, 112)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (617, 45, 24, 113)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (618, 53, 13, 11)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (619, 45, 23, 114)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (620, 45, 24, 115)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (621, 53, 6, 12)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (622, 53, 20, 13)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (623, 45, 5, 116)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (624, 45, 11, 117)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (625, 45, 11, 118)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (626, 45, 10, 119)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (627, 45, 9, 120)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (628, 53, 11, 14)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (629, 45, 9, 121)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (630, 53, 15, 15)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (631, 53, 6, 16)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (632, 45, 5, 122)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (633, 45, 18, 123)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (634, 53, 15, 17)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (635, 45, 6, 124)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (636, 53, 6, 18)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (637, 45, 7, 125)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (638, 53, 11, 19)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (639, 45, 11, 126)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (640, 53, 5, 20)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (641, 45, 7, 127)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (642, 53, 19, 21)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (643, 45, 10, 128)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (644, 53, 6, 22)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (645, 45, 7, 129)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (646, 53, 9, 23)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (647, 45, 5, 130)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (648, 53, 6, 24)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (649, 45, 7, 131)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (650, 53, 7, 25)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (651, 45, 10, 132)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (652, 53, 5, 26)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (653, 45, 9, 133)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (654, 53, 7, 27)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (655, 45, 9, 134)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (656, 53, 5, 28)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (657, 45, 14, 135)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (658, 53, 16, 29)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (659, 69, 12, 0)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (660, 69, 7, 1)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (661, 53, 5, 30)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (662, 53, 9, 31)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (663, 53, 16, 32)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (664, 53, 6, 33)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (665, 53, 5, 34)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (666, 53, 5, 35)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (667, 53, 20, 36)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (668, 53, 7, 37)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (669, 53, 6, 38)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (670, 53, 14, 39)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (671, 53, 7, 40)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (672, 53, 11, 41)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (673, 53, 15, 42)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (674, 53, 11, 43)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (675, 53, 19, 44)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (676, 53, 7, 45)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (677, 53, 9, 46)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (678, 53, 16, 47)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (679, 53, 17, 48)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (680, 53, 7, 49)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (681, 53, 10, 50)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (682, 53, 11, 51)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (683, 53, 14, 52)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (684, 53, 19, 53)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (685, 53, 15, 54)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (686, 53, 15, 55)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (687, 53, 18, 56)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (688, 53, 6, 57)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (689, 53, 17, 58)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (690, 53, 18, 59)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (691, 53, 16, 60)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (692, 53, 15, 61)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (693, 53, 11, 62)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (694, 53, 17, 63)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (695, 53, 10, 64)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (696, 53, 16, 65)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (697, 53, 11, 66)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (698, 53, 15, 67)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (699, 53, 13, 68)
GO
print 'Processed 200 total records'
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (700, 53, 14, 69)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (701, 53, 6, 70)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (702, 53, 18, 71)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (703, 53, 11, 72)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (704, 53, 18, 73)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (705, 53, 18, 74)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (706, 53, 6, 75)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (707, 53, 13, 76)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (708, 53, 18, 77)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (709, 53, 14, 78)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (710, 53, 10, 79)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (711, 53, 17, 80)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (712, 53, 16, 81)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (713, 53, 19, 82)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (714, 53, 18, 83)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (715, 53, 20, 84)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (716, 53, 6, 85)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (717, 53, 17, 86)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (718, 53, 11, 87)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (719, 53, 6, 88)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (720, 53, 16, 89)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (721, 45, 7, 136)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (722, 45, 5, 137)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (723, 45, 7, 138)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (724, 45, 5, 139)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (725, 45, 5, 140)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (726, 45, 6, 141)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (727, 45, 7, 142)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (728, 45, 5, 143)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (729, 45, 7, 144)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (730, 45, 11, 145)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (731, 45, 7, 146)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (732, 45, 9, 147)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (733, 45, 11, 148)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (734, 45, 11, 149)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (735, 45, 7, 150)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (736, 45, 9, 151)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (737, 45, 11, 152)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (738, 45, 9, 153)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (739, 45, 9, 154)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (740, 45, 9, 155)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (741, 45, 9, 156)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (742, 45, 11, 157)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (743, 45, 18, 158)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (744, 45, 10, 159)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (745, 45, 18, 160)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (746, 45, 11, 161)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (747, 45, 13, 162)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (748, 45, 15, 163)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (749, 45, 15, 164)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (750, 45, 9, 165)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (751, 45, 15, 166)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (752, 45, 18, 167)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (753, 45, 15, 168)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (754, 45, 17, 169)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (755, 45, 19, 170)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (756, 45, 18, 171)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (757, 45, 18, 172)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (758, 45, 15, 173)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (759, 45, 13, 174)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (760, 45, 15, 175)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (761, 45, 16, 176)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (762, 45, 16, 177)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (763, 45, 18, 178)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (764, 45, 16, 179)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (765, 45, 19, 180)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (766, 45, 19, 181)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (767, 45, 19, 182)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (768, 45, 16, 183)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (769, 45, 16, 184)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (770, 45, 20, 185)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (771, 45, 17, 186)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (772, 45, 21, 187)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (773, 45, 22, 188)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (774, 45, 16, 189)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (775, 45, 23, 190)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (776, 45, 22, 191)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (777, 45, 16, 192)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (778, 45, 16, 193)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (779, 45, 22, 194)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (780, 45, 22, 195)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (781, 45, 16, 196)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (782, 45, 16, 197)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (783, 45, 20, 198)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (784, 45, 20, 199)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (785, 45, 22, 200)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (786, 45, 17, 201)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (787, 45, 16, 202)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (788, 45, 16, 203)
INSERT [dbo].[characterCards] ([id], [charId], [cardId], [slot]) VALUES (789, 45, 19, 204)
SET IDENTITY_INSERT [dbo].[characterCards] OFF
/****** Object:  Table [dbo].[cards]    Script Date: 03/17/2015 16:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cards](
	[id] [int] NOT NULL,
	[cardName] [nvarchar](50) NOT NULL,
	[type] [int] NOT NULL,
	[hp] [int] NOT NULL,
	[dmg] [int] NOT NULL,
	[def] [int] NOT NULL,
	[rare] [int] NOT NULL,
	[minLevel] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (1, N'Берас', 0, 8, 5, 5, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (2, N'Эвиин', 0, 6, 6, 3, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (3, N'Эйлад', 0, 7, 7, 3, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (4, N'Маэдар', 0, 8, 4, 5, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (5, N'Воин', 1, 5, 4, 4, 1, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (6, N'Лучник', 1, 4, 5, 3, 1, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (7, N'Волшебник', 1, 3, 7, 2, 1, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (8, N'Саисса', 0, 7, 6, 4, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (9, N'Тёмный Лучник', 1, 5, 6, 4, 2, 3)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (10, N'Элементальный маг', 1, 4, 7, 3, 2, 3)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (11, N'Рыцарь', 1, 6, 5, 5, 2, 3)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (12, N'Валадор', 0, 8, 6, 4, 0, 1)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (13, N'Палладин', 1, 7, 6, 6, 2, 5)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (14, N'Страж', 1, 8, 5, 6, 2, 6)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (15, N'Ведьма', 1, 5, 8, 4, 2, 5)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (16, N'Азриэль', 1, 9, 6, 7, 3, 6)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (17, N'Свирепый Волк', 1, 7, 6, 5, 3, 6)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (18, N'Амфисбена', 1, 6, 7, 5, 2, 5)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (19, N'Ламия', 1, 6, 9, 5, 3, 6)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (20, N'Огненная Колдунья', 1, 6, 8, 4, 2, 6)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (21, N'Эльфийка', 1, 7, 8, 6, 2, 8)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (22, N'Стрелок', 1, 7, 9, 5, 3, 8)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (23, N'Воин Света', 1, 9, 6, 7, 2, 8)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (24, N'Принцесса', 1, 7, 9, 5, 2, 10)
INSERT [dbo].[cards] ([id], [cardName], [type], [hp], [dmg], [def], [rare], [minLevel]) VALUES (25, N'Охотница', 1, 8, 10, 6, 3, 10)
/****** Object:  Table [dbo].[accounts]    Script Date: 03/17/2015 16:03:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[accounts](
	[account] [varchar](50) NOT NULL,
	[password] [varchar](255) NOT NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[account] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'123345', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'12345', N'12345')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'1234567890', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'3456789', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'asdfhgj', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'bill', N'12345')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'bill1', N'12345')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'bill111', N'12345')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'billr', N'12345')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'dies1233', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'dsfgh', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'dWAHJstas51447d', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'erfgthj', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'ergth', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'esfgrydht', N'sgdhfhg')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'karl13', N'1313')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'mshdn', N'msh24578')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'nerk13', N'nekr13')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'qwerghjk', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'sadfbg', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'safgdrhtfyu', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'sdfsdfdsg', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'sdgyui', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'sfdgfhj', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'stas514', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'stas5144', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'stas514457', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'stas51447', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'stas51447d', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'wertgyuj', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'werty', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'wertyu', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'wqeryui', N'wertyui')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'wrertyui', N'123456')
INSERT [dbo].[accounts] ([account], [password]) VALUES (N'zet35040', N'123456')
/****** Object:  Default [DF_cards_type]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[cards] ADD  CONSTRAINT [DF_cards_type]  DEFAULT ((1)) FOR [type]
GO
/****** Object:  Default [DF_cards_rare]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[cards] ADD  CONSTRAINT [DF_cards_rare]  DEFAULT ((1)) FOR [rare]
GO
/****** Object:  Default [DF_cards_minLevel]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[cards] ADD  CONSTRAINT [DF_cards_minLevel]  DEFAULT ((1)) FOR [minLevel]
GO
/****** Object:  Default [DF_characterCards_slot]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characterCards] ADD  CONSTRAINT [DF_characterCards_slot]  DEFAULT ((-1)) FOR [slot]
GO
/****** Object:  Default [DF_characters_charLevel]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characters] ADD  CONSTRAINT [DF_characters_charLevel]  DEFAULT ((1)) FOR [charLevel]
GO
/****** Object:  Default [DF_characters_exp]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characters] ADD  CONSTRAINT [DF_characters_exp]  DEFAULT ((0)) FOR [exp]
GO
/****** Object:  Default [DF_characters_games]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characters] ADD  CONSTRAINT [DF_characters_games]  DEFAULT ((0)) FOR [games]
GO
/****** Object:  Default [DF_characters_win]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characters] ADD  CONSTRAINT [DF_characters_win]  DEFAULT ((0)) FOR [win]
GO
/****** Object:  Default [DF_characters_score]    Script Date: 03/17/2015 16:03:40 ******/
ALTER TABLE [dbo].[characters] ADD  CONSTRAINT [DF_characters_score]  DEFAULT ((0)) FOR [score]
GO
