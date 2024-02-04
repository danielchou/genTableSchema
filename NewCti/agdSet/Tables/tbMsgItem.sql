CREATE TABLE [agdSet].[tbMsgItem](
	[SeqNo] [smallint] IDENTITY(1,1) NOT NULL,	/*流水號*/
	[MsgItemID] [varchar](3)  NOT NULL,	/*留言類型編號*/
	[MsgItemName] [nvarchar](50)  NOT NULL,	/*留言類型名稱*/
	[ProcessType] [char](1)  NOT NULL,	/*處理方式*/
	[IsEnable] [bit]  NOT NULL,	/*是否啟用?*/
	[GeneralSkill] [varchar](50)  NULL,	/*一般服務技能*/
	[GeneralHour] [smallint]  NULL,	/*一般處理時效*/
	[SensitiveSkill] [varchar](50)  NULL,	/*敏感詞服務技能*/
	[SensitiveHour] [smallint]  NULL,	/*敏感詞處理時效*/
	[FormTypeParentID] [smallint]  NULL,	/*表單類型*/
	[FormTypeID] [smallint]  NULL,	/*表單子類型*/
	[Subject] [nvarchar](100)  NULL,	/*主旨*/
	[Description] [nvarchar](2000)  NULL,	/*描述*/
	[Priority] [tinyint]  NULL,	/*優先順序*/
	[Handler] [varchar](11)  NULL,	/*預設填表人員編*/
	[HandlerName] [nvarchar](60)  NULL,	/*預設填表人姓名*/
	[DeptSystemCode] [varchar](4)  NULL,	/*預設傳送處理單位科別系統代碼*/
	[DeptCode] [varchar](10)  NULL,	/*預設傳送處理單位代碼*/
	[DeptName] [nvarchar](20)  NULL,	/*預設傳送處理單位名稱*/
	[CreateDT] [datetime2](7)  NOT NULL,	/*建立時間*/
	[Creator] [varchar](11)  NOT NULL,	/*建立者*/
	[CreatorName] [nvarchar](60)  NOT NULL,	/*建立人員*/
	[UpdateDT] [datetime2](7)  NOT NULL,	/*更新時間*/
	[Updater] [varchar](11)  NOT NULL,	/*更新者*/
	[UpdaterName] [nvarchar](60)  NOT NULL,	/*更新人員*/
 CONSTRAINT [PK_tbMsgItem] PRIMARY KEY CLUSTERED 
(
	[MsgItemID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流水號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'SeqNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言類型編號' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'MsgItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'留言類型名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'MsgItemName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'處理方式 : G:Genesys, O:顧客關係維護單' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'ProcessType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否啟用?' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'IsEnable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一般服務技能' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'GeneralSkill'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一般處理時效' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'GeneralHour'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'敏感詞服務技能' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'SensitiveSkill'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'敏感詞處理時效' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'SensitiveHour'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表單類型' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'FormTypeParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表單子類型' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'FormTypeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主旨' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Subject'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'優先順序 : 1: 一般件, 2: 速件, 3: 特殊件' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Priority'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'預設填表人員編' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Handler'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'預設填表人姓名' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'HandlerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'預設傳送處理單位科別系統代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'DeptSystemCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'預設傳送處理單位代碼' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'DeptCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'預設傳送處理單位名稱' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'DeptName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'CreateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'建立人員' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'CreatorName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新時間' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'UpdateDT'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新者' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'Updater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新人員' ,@level0type=N'SCHEMA',@level0name=N'agdSet', @level1type=N'TABLE',@level1name=N'tbMsgItem', @level2type=N'COLUMN',@level2name=N'UpdaterName'
GO