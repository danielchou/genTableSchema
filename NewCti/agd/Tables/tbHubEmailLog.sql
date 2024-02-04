CREATE TABLE [agd].[tbHubEmailLog](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[ParentSeqNo] [int]  NOT NULL,	/*媽媽流水號*/
	[InteractionID] [varchar](20)  NOT NULL,	/*Interaction編號*/
	[AgentLoginID] [varchar](10)  NOT NULL,	/*CTI登入帳號*/
	[UserID] [varchar](11)  NOT NULL,	/*員工編號*/
	[Type] [char](1)  NOT NULL,	/*類型*/
	[Status] [char](1)  NOT NULL,	/*狀態*/
	[Action] [char](1)  NOT NULL,	/*動作*/
	[BeginDT] [datetime2](7)  NULL,	/*開始時間*/
	[EndDT] [datetime2](7)  NULL,	/*結束時間*/
	[Duration] [int]  NULL,	/*服務時長(秒)*/
	[FinalEndDT] [datetime2](7)  NULL,	/*最終結束時間*/
	[FinalDuration] [int]  NULL,	/*服務時長(秒)*/
	[CustKey] [numeric](38,0)  NULL,	/*顧客識別流水號*/
	[CustomerID] [varchar](20)  NULL,	/*顧客ID*/
	[CustomerName] [nvarchar](200)  NULL,	/*顧客姓名*/
	[ContactID] [int]  NULL,	/*服務紀錄ID*/
	[MsgSn] [int]  NULL,	/*留言編號*/
	[Memo] [nvarchar](500)  NULL,	/*其他註解*/
	[SenderEmail] [nvarchar](200)  NOT NULL,	/*寄件人Email*/
	[Subject] [nvarchar](200)  NOT NULL,	/*主旨*/
	[Content] [nvarchar](4000)  NOT NULL,	/*本文*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者(Hub HostName)*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者(Hub HostName)*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbHubEmailLog] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'媽媽流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'ParentSeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Interaction編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'InteractionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CTI登入帳號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'AgentLoginID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'員工編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'類型 : 0:EmailIn 1:EmailOut' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'狀態 : 0:Invite, 1:Talking, 2:InWorkbin, 3:End' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'動作 : 0:原始, 1:回信, 2:轉寄' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'開始時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'BeginDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'結束時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'EndDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務時長(秒)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最終結束時間 : 含小孩Email回覆時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'FinalEndDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務時長(秒) : 含小孩Email回覆時間服務時長(秒)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'FinalDuration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客識別流水號 : 預設為0，後續AGD更新' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'CustKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客ID : 空白為"New"，後續AGD更新' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客姓名 : 後續AGD更新' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務紀錄ID : 預設為0，後續AGD更新' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'ContactID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'MsgSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其他註解' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Memo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'寄件人Email' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'SenderEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主旨' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本文' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者(Hub HostName)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbHubEmailLog', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO