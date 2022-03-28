CREATE TABLE [agdSet].[tbCode](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,
	[CodeType] [nvarchar(20)]  NOT NULL,
	[CodeId] [varchar(20)]  NOT NULL,
	[CodeName] [nvarchar(50)]  NOT NULL,
	[IsEnable] [bit]  NOT NULL,
	[Creator] [varchar(20)]  NOT NULL,
	[Updator] [varchar(20)]  NOT NULL,
	[CreateDT] [datetime2(7)]  NOT NULL,
	[UpdateDT] [datetime2(7)]  NOT NULL,

 CONSTRAINT [PK_tbCode] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbCode] ADD  CONSTRAINT [DF_tbCode_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代碼分類' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統代碼檔代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統代碼檔名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CodeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'Updator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbCode', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
