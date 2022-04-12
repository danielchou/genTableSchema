CREATE TABLE [agdSet].[tbReason](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[ReasonID] [varchar](20) NOT NULL,
	[ReasonName] [nvarchar](50) NOT NULL,
	[ParentReasonID] [varchar](20) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[BussinessUnit] [varchar](3) NULL,
	[BussinessB03Type] [varchar](3) NULL,
	[ReviewType] [varchar](3) NULL,
	[Memo] [nvarchar](20) NULL,
	[WebUrl] [nvarchar](200) NULL,
	[KMUrl] [nvarchar](200) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsUsually] [bit] NOT NULL,
	[UsuallyReasonName] [nvarchar](50) NULL,
	[UsuallyDisplayOrder] [int] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbReason] PRIMARY KEY CLUSTERED 
(
	[ReasonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbReason] ADD  CONSTRAINT [DF_tbReason_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因碼代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'ReasonID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'聯繫原因碼名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'ReasonName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上層聯繫原因碼代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'ParentReasonID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'階層' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'事業處 : 於共用代碼維護' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'BussinessUnit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'B03業務別 : 於共用代碼維護' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'BussinessB03Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'覆核類別 : 於共用代碼維護' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'ReviewType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'網頁連結' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'WebUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KM連結' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'KMUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否常用' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'IsUsually'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'常用名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'UsuallyReasonName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'常用顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'UsuallyDisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbReason', @level2type=N'COLUMN',@level2name=N'Updator'
GO