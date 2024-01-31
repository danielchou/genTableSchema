CREATE TABLE [agd].[tbWebMsg](
	[MsgSn] [int]  NOT NULL,	/*留言編號*/
	[ContactID] [int]  NULL,	/*服務紀錄ID*/
	[InteractionID] [varchar](20)  NULL,	/*Interaction編號*/
	[Status] [varchar](2)  NOT NULL,	/*狀態*/
	[SourceCreateDate] [datetime2](7)  NULL,	/*留言資料建立日*/
	[SourceGuestID] [varchar](10)  NULL,	/*留言ID*/
	[SourceGuestName] [nvarchar](50)  NULL,	/*留言姓名*/
	[SourceGuestPhone] [varchar](30)  NULL,	/*留言電話*/
	[SourceGuestEmail] [varchar](50)  NULL,	/*留言Email*/
	[SourceMsgItemID] [varchar](3)  NULL,	/*留言類型編號*/
	[MsgItemName] [nvarchar](50)  NULL,	/*留言類型名稱*/
	[SourceGuestMessage] [nvarchar](1000)  NULL,	/*留言內容*/
	[IsSensitive] [bit]  NOT NULL,	/*是否含敏感詞?*/
	[SensitivePhrase] [nvarchar](10)  NULL,	/*掃到之敏感詞*/
	[Skill] [varchar](50)  NULL,	/*服務技能代碼*/
	[OverdueDT] [datetime2](7)  NULL,	/*逾期處理時效*/
	[IsOverdueNotified] [bit]  NOT NULL,	/*是否逾期已通知公告?*/
	[OverdueNotifiedDT] [datetime2](7)  NULL,	/*逾期通知公告時間*/
	[BeginOwner] [varchar](11)  NULL,	/*Genesys下派之處理人*/
	[BeginOwnerName] [nvarchar](60)  NULL,	/*Genesys下派之處理人姓名*/
	[BeginDT] [datetime2](7)  NULL,	/*Genesys下派專人服務開始時間*/
	[EndDT] [datetime2](7)  NULL,	/*結案時間*/
	[Duration] [int]  NULL,	/*服務時長*/
	[CustKey] [numeric](38,0)  NULL,	/*顧客識別流水號*/
	[CustomerID] [varchar](20)  NULL,	/*顧客ID*/
	[CustomerName] [nvarchar](200)  NULL,	/*顧客姓名*/
	[Owner] [varchar](11)  NULL,	/*處理人*/
	[OwnerName] [nvarchar](60)  NULL,	/*處理人姓名*/
	[OwnerMemo] [nvarchar](100)  NULL,	/*處理人備註*/
	[FollowUpDT] [datetime2](7)  NULL,	/*續訪時間*/
	[ReplyChannel] [varchar](1)  NULL,	/*回覆方式*/
	[ReplyContent] [nvarchar](2000)  NULL,	/*回覆內容*/
	[ReplyEmail] [varchar](200)  NULL,	/*回覆Email*/
	[SubmitDT] [datetime2](7)  NULL,	/*送審時間*/
	[Reviewer] [varchar](11)  NULL,	/*審核人*/
	[ReviewerName] [nvarchar](60)  NULL,	/*審核人姓名*/
	[ReviewDT] [datetime2](7)  NULL,	/*審核時間*/
	[ReviewMemo] [nvarchar](100)  NULL,	/*退回原因*/
	[IsOwnerTransferred] [bit]  NOT NULL,	/*處理人是否移轉過?*/
	[IsReviewerTransferred] [bit]  NOT NULL,	/*審核人是否移轉過?*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](30)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](30)  NOT NULL,	/*更新者*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbWebMsg] PRIMARY KEY CLUSTERED 
(
	[MsgSn] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言編號 : 匯入ETL寫入官網資訊MsgSn' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'MsgSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務紀錄ID : 下派AGD後寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ContactID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Interaction編號 : 下派AGD後寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'InteractionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'狀態 : 00	新進件
10	分派中
20	作業中
21	暫存中
22	已退回
31	待審核
32	審核中
50	已轉開
51	已結案-電話回覆
52	已結案-Email回覆
53	已結案-人工取消' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言資料建立日 : 匯入ETL寫入官網資訊CreateDate' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceCreateDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言ID : 匯入ETL寫入官網資訊GuestID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceGuestID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言姓名 : 匯入ETL寫入官網資訊GuestName' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceGuestName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言電話 : 匯入ETL寫入官網資訊GuestPhone' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceGuestPhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言Email : 匯入ETL寫入官網資訊GuestEmail' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceGuestEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言類型編號 : 匯入ETL寫入官網資訊ItemID' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceMsgItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言類型名稱 : 匯入ETL寫入ItemID當時對應之代碼名稱' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'MsgItemName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言內容 : 匯入ETL寫入官網資訊Message' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SourceGuestMessage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否含敏感詞? : 匯入ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'IsSensitive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'掃到之敏感詞 : 匯入ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SensitivePhrase'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務技能代碼 : 匯入ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Skill'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逾期處理時效 : 匯入ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'OverdueDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否逾期已通知公告? : 預設0，逾期通知ETL寫入1' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'IsOverdueNotified'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'逾期通知公告時間 : 逾期通知ETL寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'OverdueNotifiedDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genesys下派之處理人 : 下派AGD後寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'BeginOwner'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genesys下派之處理人姓名 : 下派AGD後寫入, 移轉時update' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'BeginOwnerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Genesys下派專人服務開始時間 : 下派AGD後寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'BeginDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'結案時間 : 一開始空的, 結案才寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'EndDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服務時長 : 結案時間-開始服務時間(秒)' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Duration'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客識別流水號 : 處理人進行 暫存/送審/結案時寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'CustKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客ID : 處理人進行 暫存/送審/結案時寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'CustomerID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顧客姓名 : 處理人進行 暫存/送審/結案時寫入' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'CustomerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人 : 下派AGD後寫入, 移轉時update' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Owner'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人姓名 : 下派AGD後寫入, 移轉時update' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'OwnerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人備註' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'OwnerMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'續訪時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'FollowUpDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回覆方式 : V:電話回覆 E:Email回覆' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReplyChannel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回覆內容' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReplyContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'回覆Email' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReplyEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送審時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'SubmitDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Reviewer'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核人姓名' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReviewerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReviewDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退回原因' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'ReviewMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人是否移轉過? : 預設0, 移轉時update' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'IsOwnerTransferred'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核人是否移轉過? : 預設0, 移轉時update' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'IsReviewerTransferred'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsg', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO