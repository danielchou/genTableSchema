CREATE TABLE [agd].[tbWebMsgTransferLog](
	[SeqNo] [int]  NOT NULL,	/*流水號*/
	[MsgSn] [int]  NOT NULL,	/*留言編號*/
	[OwnerBefore] [varchar](11)  NULL,	/*移轉前處理人*/
	[OwnerAfter] [varchar](11)  NULL,	/*移轉後處理人*/
	[ReviewerBefore] [varchar](11)  NULL,	/*移轉前審核人*/
	[ReviewerAfter] [varchar](11)  NULL,	/*移轉後審核人*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](11)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
 CONSTRAINT [PK_tbWebMsgTransferLog] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'MsgSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'移轉前處理人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'OwnerBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'移轉後處理人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'OwnerAfter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'移轉前審核人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'ReviewerBefore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'移轉後審核人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'ReviewerAfter'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間 : 移轉時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者 : 移轉執行人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgTransferLog', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO