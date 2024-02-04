CREATE TABLE [agd].[tbHubChatDialogs](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[HubChatLogSeqNo] [int]  NOT NULL,	/*Chat 紀錄表流水號*/
	[DialogSeq] [int]  NOT NULL,	/*對話編號*/
	[DialogStartAtTime] [datetime2](7)  NOT NULL,	/*對話時間*/
	[DialogType] [char](1)  NOT NULL,	/*對話型態*/
	[DialogContent] [nvarchar](3000)  NOT NULL,	/*對話內容*/
	[AttendeeId] [nvarchar](20)  NOT NULL,	/*發言者ID*/
	[AttendeeNickName] [nvarchar](50)  NOT NULL,	/*發言者暱稱*/
	[AttendeeType] [char](1)  NOT NULL,	/*發言者型態*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者(Hub HostName)*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者(Hub HostName)*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbHubChatDialogs] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Chat 紀錄表流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'HubChatLogSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對話編號 : 用於排序使用' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'DialogSeq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對話時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'DialogStartAtTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對話型態 : 0:NewParty, 1:PartyLeft, 2:Message, 3:Notice' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'DialogType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'對話內容' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'DialogContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發言者ID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'AttendeeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發言者暱稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'AttendeeNickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'發言者型態 : 0:Client, 1:Agent, 2:Supervisor' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'AttendeeType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubChatDialogs', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO