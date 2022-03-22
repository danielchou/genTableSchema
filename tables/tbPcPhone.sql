CREATE TABLE [agdSet].[tbPcPhone](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,
	[ExtCode] [nvarchar(20)]  NOT NULL,
	[ComputerName] [nvarchar(50)]  NOT NULL,
	[ComputerIP] [nvarchar(50)]  NOT NULL,
	[Memo] [nvarchar(600)]  NOT NULL,
	[IsEnable] [bit]  NOT NULL,
	[Creator] [nvarchar(20)]  NOT NULL,
	[Updator] [nvarchar(20)]  NOT NULL,
	[CreateDT] [datetime2(7)]  NOT NULL,
	[UpdateDT] [datetime2(7)]  NOT NULL,

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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分機號碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ExtCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ComputerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'電腦IP' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'ComputerIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'備註' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N''ALL':全部, '1':啟用, '0':停用' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'Updator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'異動時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbPcPhone', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
