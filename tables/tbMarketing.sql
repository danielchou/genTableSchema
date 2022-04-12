CREATE TABLE [agdSet].[tbMarketing](
	[SeqNo] [int] IDENTITY(1,1)NOT NULL,
	[MarketingID] [varchar](20) NOT NULL,
	[MarketingType] [varchar](1) NOT NULL,
	[MarketingName] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](100) NOT NULL,
	[MarketingScript] [nvarchar](2000) NULL,
	[MarketingBegintDT] [datetime2](7) NOT NULL,
	[MarketingEndDT] [datetime2](7) NOT NULL,
	[OfferCode] [varchar](20) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsEnable] [bit] NOT NULL,
	[CreateDT] [datetime2](7) NOT NULL,
	[Creator] [varchar](20) NOT NULL,
	[UpdateDT] [datetime2](7) NOT NULL,
	[Updator] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tbMarketing] PRIMARY KEY CLUSTERED 
(
	[MarketingID] ASC,
	[MarketingType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbMarketing] ADD  CONSTRAINT [DF_tbMarketing_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行銷方案代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行銷方案類別 :  1:客群方案, 2:全面廣宣, 3:未行銷' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行銷方案名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行銷方案內容' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行銷方案話術' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingScript'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'開始日期' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingBegintDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'結束日期' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'MarketingEndDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'專案識別碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'OfferCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMarketing', @level2type=N'COLUMN',@level2name=N'Updator'
GO