CREATE TABLE [agd].[tbMediaIXProc](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[Source] [char](1)  NOT NULL,	/*來源*/
	[Status] [char](1)  NOT NULL,	/*狀態*/
	[MsgSn] [int]  NOT NULL,	/*留言編號*/
	[SourceCreateDate] [datetime2](7)  NULL,	/*留言資料建立日*/
	[SourceGuestID] [varchar](10)  NULL,	/*留言ID*/
	[SourceGuestName] [nvarchar](50)  NULL,	/*留言姓名*/
	[SourceGuestPhone] [varchar](30)  NULL,	/*留言電話*/
	[SourceGuestEmail] [varchar](50)  NULL,	/*留言Email*/
	[Skill] [varchar](50)  NULL,	/*服務技能代碼*/
	[ChatInSeqNo] [int]  NULL,	/*tbChatIn流水號*/
	[MediaServerName] [varchar](30)  NULL,	/*MediaServer名稱*/
	[InteractionDT] [datetime2](7)  NULL,	/*Interaction成案時間*/
	[InteractionID] [varchar](20)  NULL,	/*Interaction編號*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbMediaIXProc] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'來源 : 0:模擬"訪客留言板" 1:正式"訪客留言板" 2:模擬"Chat" 3:正式"Chat" ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'Source'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'狀態 : 0:新進件
1:準備中(Media Server已讀取準備送至Genesys)
2:分派中(Genesys成案分派中)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'MsgSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言資料建立日' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SourceCreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SourceGuestID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言姓名' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SourceGuestName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言電話' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SourceGuestPhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言Email' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'SourceGuestEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務技能代碼' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'Skill'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'tbChatIn流水號 : 保留給未來Chat整合使用' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'ChatInSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'MediaServer名稱 : 已成案之負責MediaServer名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'MediaServerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Interaction成案時間 : Genesys 成案時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'InteractionDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Interaction編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'InteractionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbMediaIXProc', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO