CREATE TABLE [agdSet].[tbPcPhone](
	[SeqNo] [INT] IDENTITY(1,1) NOT NULL
	[ExtCode] [NVARCHAR(10)]  NOT NULL
	[ComputerName] [NVARCHAR(25)]  NOT NULL
	[ComputerIp] [NVARCHAR(23)]  NOT NULL
	[Memo] [NVARCHAR(600)]  NOT NULL
	[IsEnable] [BIT]  NOT NULL
	[Creator] [VARCHAR(20)]  NOT NULL
	[Updator] [VARCHAR(20)]  NOT NULL
	[CreateDt] [DATETIME2]  NOT NULL
	[UpdateDt] [DATETIME2]  NOT NULL
 CONSTRAINT [PK_tbPcPhone] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [agdSet].[tbPcPhone] ADD  CONSTRAINT [DF_tbPcPhone_Creator]  DEFAULT ((1)) FOR [Creator]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Seq No.' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電話分機 : AAAAA' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ExtCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦名稱 : 經辦的電腦名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ComputerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦IP' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ComputerIp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用? : ALL | 1:是 | 0:否 ' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Updator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'CreateDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'UpdateDt'
GO