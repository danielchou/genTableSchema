CREATE TABLE [agdSet].[tbSysConfig](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[SysConfigType] [varchar](20) NOT NULL,
	[SysConfigID] [varchar](20) NOT NULL,
	[SysConfigName] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](200) NOT NULL,
	[IsVisible] [bit] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbSysConfig] PRIMARY KEY CLUSTERED 
(
	[SysConfigType] ASC,
	[SysConfigID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbSysConfig] ADD  CONSTRAINT [DF_tbSysConfig_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統參數類別 : 於共用代碼維護' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'SysConfigType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統參數代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'SysConfigID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統參數名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'SysConfigName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系統參數內容' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否顯示?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'IsVisible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbSysConfig', @level2type=N'COLUMN',@level2name=N'Updator'
GO