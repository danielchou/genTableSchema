CREATE TABLE [agdSet].[tbFunc](
	[SeqNo] [INT] IDENTITY(1,1) NOT NULL
	[FuncId] [VARCHAR(20)]  NOT NULL
	[FuncName] [NVARCHAR(50)]  NOT NULL
	[FuncPath] [NVARCHAR(20)]  NOT NULL
	[FuncIcon] [NVARCHAR(20)]  NOT NULL
	[IsEnable] [BIT]  NOT NULL
	[Creator] [VARCHAR(20)]  NOT NULL
	[Updator] [VARCHAR(20)]  NOT NULL
	[CreateDt] [DATETIME2]  NOT NULL
	[UpdateDt] [DATETIME2]  NOT NULL
 CONSTRAINT [PK_tbFunc] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbFunc] ADD  CONSTRAINT [DF_tbFunc_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能ID' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能路由' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能圖示' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'Updator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'CreateDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'UpdateDt'
GO