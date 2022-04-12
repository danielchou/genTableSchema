CREATE TABLE [agdSet].[tbCode](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[CodeType] [varchar](20) NOT NULL,
	[CodeID] [varchar](20) NOT NULL,
	[CodeName] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](500) NULL,
	[Memo] [nvarchar](200) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbCode] PRIMARY KEY CLUSTERED 
(
	[CodeType] ASC,
	[CodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbCode] ADD  CONSTRAINT [DF_tbCode_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'共用碼類別' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'共用碼代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'共用碼名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'共用碼內容' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Updator'
GO