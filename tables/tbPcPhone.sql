CREATE TABLE [agdSet].[tbPcPhone](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,
	[extCode] [nvarchar(20)]  NOT NULL,
	[computerName] [nvarchar(50)]  NOT NULL,
	[computerIP] [nvarchar(50)]  NOT NULL,
	[memo] [nvarchar(600)]  NOT NULL,
	[isEnable] [bit]  NOT NULL,
	[creator] [varchar(20)]  NOT NULL,
	[updator] [varchar(20)]  NOT NULL,
	[createDT] [datetime2(7)]  NOT NULL,
	[updateDT] [datetime2(7)]  NOT NULL,

 CONSTRAINT [PK_tbPcPhone] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbPcPhone] ADD  CONSTRAINT [DF_tbPcPhone_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分機號碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'extCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'computerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦IP' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'computerIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'isEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'updator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'createDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'updateDT'
GO
