CREATE TABLE [agd].[tbWebMsgReviewLog](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[MsgSn] [int]  NOT NULL,	/*留言編號*/
	[Owner] [varchar](11)  NULL,	/*處理人*/
	[OwnerName] [nvarchar](60)  NULL,	/*處理人姓名*/
	[SubmitDT] [datetime2](7)  NULL,	/*送審時間*/
	[ReviewResult] [varchar](11)  NULL,	/*審核結果*/
	[ReviewMemo] [nvarchar](100)  NULL,	/*退回原因*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](11)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
 CONSTRAINT [PK_tbWebMsgReviewLog] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言編號' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'MsgSn'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'Owner'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理人姓名' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'OwnerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'送審時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'SubmitDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'審核結果 : A:同意/R:退回' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'ReviewResult'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'退回原因' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'ReviewMemo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間 : 審核時間' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者 : 審核人' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員 : 審核人姓名' ,@level0type=N'SCHEMA',@level0name=N'agd', @level1type=N'TABLE',@level1name=N'tbWebMsgReviewLog', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO