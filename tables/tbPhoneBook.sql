CREATE TABLE [agdSet].[tbPhoneBook](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[PhoneBookID] [varchar](20) NOT NULL,
	[PhoneBookName] [nvarchar](50) NOT NULL,
	[ParentPhoneBookID] [varchar](20) NOT NULL,
	[PhoneBookNumber] [varchar](20) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[Memo] [nvarchar](200) NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbPhoneBook] PRIMARY KEY CLUSTERED 
(
	[PhoneBookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbPhoneBook] ADD  CONSTRAINT [DF_tbPhoneBook_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電話簿代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'PhoneBookID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電話簿名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'PhoneBookName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上層電話簿代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'ParentPhoneBookID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電話號碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'PhoneBookNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'階層' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPhoneBook', @level2type=N'COLUMN',@level2name=N'Updator'
GO