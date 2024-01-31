CREATE TABLE [agdSet].[tbMsgEmailTemplete](
	[SeqNo] [smallint]  NOT NULL,	/*流水號*/
	[MsgEmailTempleteID] [smallint]  NOT NULL,	/*留言Email回覆範本ID*/
	[MsgEmailTempleteName] [nvarchar](50)  NOT NULL,	/*留言Email回覆範本名稱*/
	[ParentMsgEmailTempleteID] [smallint]  NOT NULL,	/*留言Email回覆範本父類ID*/
	[Level] [tinyint]  NOT NULL,	/*階層*/
	[Description] [nvarchar](1000)  NULL,	/*描述*/
	[DisplayOrder] [smallint]  NOT NULL,	/*顯示順序*/
	[IsEnable] [bit]  NOT NULL,	/*是否啟用?*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](11)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](11)  NOT NULL,	/*更新者*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbMsgEmailTemplete] PRIMARY KEY CLUSTERED 
(
	[MsgEmailTempleteID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言Email回覆範本ID : L1, L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'MsgEmailTempleteID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言Email回覆範本名稱 : L1分類名稱, L2範本名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'MsgEmailTempleteName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言Email回覆範本父類ID : L1(ROOT), L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'ParentMsgEmailTempleteID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'階層 : L1, L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述 : L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顯示順序 : L1, L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'DisplayOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用? : L1, L2' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgEmailTemplete', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO