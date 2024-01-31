CREATE TABLE [agd].[tbHubEmailAttenden](
	[SeqNo] [int]  NOT NULL,	/*流水號*/
	[HubEmailLogSeqNo] [int]  NOT NULL,	/*Email 紀錄表流水號*/
	[Type] [char](1)  NOT NULL,	/*類型*/
	[AttendeeSeq] [int]  NOT NULL,	/*收件人編號*/
	[AttendeeDisplay] [nvarchar](50)  NULL,	/*收件人顯示名稱*/
	[AttendeeEmail] [nvarchar](200)  NOT NULL,	/*收件人Email*/
	[AttendeeMemo] [nvarchar](300)  NULL,	/*收件人註解*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者(Hub HostName)*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者(Hub HostName)*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbHubEmailAttenden] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Email 紀錄表流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'HubEmailLogSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'類型 : 0:收件人(TO) 1:副本(CC) 2:密件(BCC)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人編號 : 用於排序使用' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'AttendeeSeq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人顯示名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'AttendeeDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人Email' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'AttendeeEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人註解' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'AttendeeMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailAttenden', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO