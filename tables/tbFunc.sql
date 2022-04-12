CREATE TABLE [agdSet].[tbFunc](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[FuncID] [varchar](20) NOT NULL,
	[FuncName] [nvarchar](50) NOT NULL,
	[ParentFuncID] [varchar](20) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[SystemType] [varchar](20) NOT NULL,
	[IconName] [varchar](20) NULL,
	[RouteName] [varchar](50) NULL,
	[DisplayOrder] [int] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbFunc] PRIMARY KEY CLUSTERED 
(
	[FuncID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbFunc] ADD  CONSTRAINT [DF_tbFunc_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'FuncName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上層功能代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'ParentFuncID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'階層' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統類別 : AGD, Supervisor, Recording, Monitor' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'SystemType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Icon名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'IconName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'RouteName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbFunc', @level2type=N'COLUMN',@level2name=N'Updator'
GO