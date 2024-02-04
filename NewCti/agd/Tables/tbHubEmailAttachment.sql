CREATE TABLE [agd].[tbHubEmailAttachment](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[HubEmailLogSeqNo] [int]  NOT NULL,	/*Email 紀錄表流水號*/
	[FileSeq] [int]  NOT NULL,	/*檔案編號*/
	[FileName] [nvarchar](200)  NOT NULL,	/*檔案名稱*/
	[FileExtension] [nvarchar](30)  NOT NULL,	/*檔案副檔名*/
	[FileSize] [int]  NOT NULL,	/*檔案長度*/
	[FileContent] [varbinary](max)  NOT NULL,	/*檔案內容*/
	[FileMemo] [nvarchar](300)  NULL,	/*檔案註解*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者(Hub HostName)*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者(Hub HostName)*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbHubEmailAttachment] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email 紀錄表流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'HubEmailLogSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案編號 : 用於排序使用' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileSeq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案副檔名' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileExtension'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案長度' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案內容' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'檔案註解' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'FileMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttachment', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO